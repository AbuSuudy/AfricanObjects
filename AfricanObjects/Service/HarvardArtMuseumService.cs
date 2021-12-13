using AfricanObjects.Interface;
using AfricanObjects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{
    public class HarvardArtMuseumService : IMuseumService
    {
        private HttpClient client;
        private static Random rnd = new Random();
        private static int? pageSize;
        private readonly string HARVARD_API_KEY = Environment.GetEnvironmentVariable("HARVARD_API_KEY");

        public HarvardArtMuseumService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("HarvardArtMuseums");
         
        }

        public async Task GetMaxRange()
        {
            HarvardArtMuseum museumObject = new HarvardArtMuseum();

            var request = new HttpRequestMessage(HttpMethod.Get, $"/object?apikey={HARVARD_API_KEY}&size=1&page=1&place=2029200");

            HttpResponseMessage responseObject = await client.SendAsync(request);

            if (responseObject.IsSuccessStatusCode)
            {
                museumObject = JsonConvert.DeserializeObject<HarvardArtMuseum>(await responseObject.Content.ReadAsStringAsync());

                pageSize = museumObject.info.pages;
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

                int pageNumber = rnd.Next(0, pageSize.Value);

                var request = new HttpRequestMessage(HttpMethod.Get, string.Format($"/object?apikey={HARVARD_API_KEY}&size=1&page={pageNumber}&place=2029200"));

                HttpResponseMessage responseObject = await client.SendAsync(request);

                if (responseObject.IsSuccessStatusCode)
                {

                    HarvardArtMuseum harvardArtObj = JsonConvert.DeserializeObject<HarvardArtMuseum>(await responseObject.Content.ReadAsStringAsync());

                    int id = harvardArtObj.records.FirstOrDefault().id;
                    var masterRecordRequest = new HttpRequestMessage(HttpMethod.Get, string.Format($"/object/{id}?apikey={HARVARD_API_KEY}"));

                    HttpResponseMessage masterResponse = await client.SendAsync(masterRecordRequest);

                    if (masterResponse.IsSuccessStatusCode)
                    {

                        RecordMaster recordMaster = JsonConvert.DeserializeObject<RecordMaster>(await masterResponse.Content.ReadAsStringAsync());

                        if (recordMaster?.Images?.Count == 0)
                        {
                            return null;
                        }

                        museumObject.Title = recordMaster.Title;
                        museumObject.Culture = recordMaster.Culture;
                        museumObject.Location = recordMaster.Places.FirstOrDefault().Displayname;
                        museumObject.objectDate = recordMaster.Dated;
                        museumObject.objectURL = recordMaster.Url;
                        museumObject.objectImage = recordMaster.Images.FirstOrDefault().Baseimageurl;
                        museumObject.Country = recordMaster.Terms.Place.ElementAt(1).Name;
                        museumObject.Source = "Harvard Art Museums";

                    }




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
