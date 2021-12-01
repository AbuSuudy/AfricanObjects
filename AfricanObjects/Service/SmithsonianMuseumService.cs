﻿using AfricanObjects.Interface;
using AfricanObjects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{
    public class SmithsonianMuseumService : IMuseumService
    {
        HttpClient client;
        static Random rnd = new Random();
        static int? pageSize;

        string SMITHSONIAN_API_KEY = Environment.GetEnvironmentVariable("SMITHSONIAN_API_KEY");

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

        public async Task<TweetObject> GetMuseumObject()
        {
            try
            {
                TweetObject museumObject = new TweetObject();

                if (pageSize == null)
                {
                    await GetMaxRange();
                }

                int pageNumber = rnd.Next(0, pageSize.Value);

                var request = new HttpRequestMessage(HttpMethod.Get, string.Format($"search?api_key={SMITHSONIAN_API_KEY}&rows=1&start={pageNumber}&q=unit_code:NMAfA"));

                HttpResponseMessage responseObject = await client.SendAsync(request);

                if (responseObject.IsSuccessStatusCode)
                {
                    SmithsonianMuseumObject smithsonianMuseumObject = JsonConvert.DeserializeObject<SmithsonianMuseumObject>(await responseObject.Content.ReadAsStringAsync());
     

                    if (smithsonianMuseumObject.response.rows.FirstOrDefault().content.descriptiveNonRepeating.online_media.mediaCount == 0)
                    {
                        return null;
                    }

                    museumObject.Title = smithsonianMuseumObject.response.rows.FirstOrDefault().title;
                    museumObject.Location = smithsonianMuseumObject.response.rows.FirstOrDefault().content.freetext.place.FirstOrDefault(x => x.label == "Geography").content;
                    museumObject.objectDate = smithsonianMuseumObject.response.rows.FirstOrDefault().content.freetext.date.FirstOrDefault(x => x.label == "Date").content;
                    museumObject.objectURL = smithsonianMuseumObject.response.rows.FirstOrDefault().content.descriptiveNonRepeating.record_link;
                    museumObject.objectImage = smithsonianMuseumObject.response.rows.FirstOrDefault().content.descriptiveNonRepeating.online_media.media.FirstOrDefault().resources.FirstOrDefault(x => x.label == "High-resolution JPEG").url;
                    museumObject.Country = smithsonianMuseumObject.response.rows.FirstOrDefault().content.indexedStructured.geoLocation.LastOrDefault()?.L2.content;

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