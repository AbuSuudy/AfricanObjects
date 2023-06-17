using AfricanObjects.Interface;
using AfricanObjects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{
    public class SmithsonianMuseumService : IMuseumService
    {
        private HttpClient client;
        private static int? pageSize;
        private readonly string SMITHSONIAN_API_KEY = Environment.GetEnvironmentVariable("SMITHSONIAN_API_KEY");

        public SmithsonianMuseumService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("Smithsonian");
            
        }

        public async Task GetMaxRange()
        {
            SmithsonianMuseumObject museumObject = new SmithsonianMuseumObject();

            var request = new HttpRequestMessage(HttpMethod.Get, $"search?api_key={SMITHSONIAN_API_KEY}&rows=1&start=1&q=unit_code:NMAfA");

            HttpResponseMessage responseObject = await client.SendAsync(request);

            if (responseObject.IsSuccessStatusCode)
            {
                museumObject = JsonConvert.DeserializeObject<SmithsonianMuseumObject>(await responseObject.Content.ReadAsStringAsync());

                pageSize = museumObject.response.rowCount;
            }
        }

        public async Task<MuseumObject> GetMuseumObject()
        {
            try
            {
                MuseumObject museumObject = new MuseumObject();

                if (pageSize == null)
                {
                    await GetMaxRange();
                }

                int pageNumber =  RandomNumberGenerator.GetInt32(0, pageSize.Value + 1);

                var request = new HttpRequestMessage(HttpMethod.Get, string.Format($"search?api_key={SMITHSONIAN_API_KEY}&rows=1&start={pageNumber}&q=unit_code:NMAfA"));

                HttpResponseMessage responseObject = await client.SendAsync(request);

                if (responseObject.IsSuccessStatusCode)
                {
                    SmithsonianMuseumObject smithsonianMuseumObject = JsonConvert.DeserializeObject<SmithsonianMuseumObject>(await responseObject.Content.ReadAsStringAsync());
     

                    if (smithsonianMuseumObject.response.rows.Count() ==0  ||smithsonianMuseumObject.response.rows.FirstOrDefault()?.content.descriptiveNonRepeating.online_media.mediaCount == 0)
                    {
                        return null;
                    }

                    var smithosnianData = smithsonianMuseumObject.response.rows.FirstOrDefault();

                    museumObject.Title = smithosnianData.title;
                    museumObject.Location = smithosnianData.content.freetext.place.FirstOrDefault(x => x.label == "Geography").content;
                    museumObject.objectDate = smithosnianData.content.freetext.date.FirstOrDefault(x => x.label == "Date").content;
                    museumObject.objectURL = $"https://www.si.edu/object/{smithosnianData.url}";
                    museumObject.objectImage = new List<string>() { smithosnianData.content.descriptiveNonRepeating.online_media.media.FirstOrDefault().resources.FirstOrDefault(x => x.label == "High-resolution JPEG").url};
                    museumObject.Country = smithosnianData.content.indexedStructured.geoLocation.LastOrDefault()?.L2.content;
                    museumObject.Source = "Smithsonian";

                }


                return museumObject;
            }
            catch (Exception ex)
            {
                return null;
            }
             }
    }
}
