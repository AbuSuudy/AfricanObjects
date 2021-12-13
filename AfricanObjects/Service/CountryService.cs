using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AfricanObjects.Interface;
using AfricanObjects.Models;

namespace AfricanObjects.Service
{
    public  class CountryService : ICountryService
    {
        private  HttpClient client;

        public CountryService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("RestCountries");
        }

        public  async Task<List<String>> GetAllAfricanCountries()
        {
            List<Countries> listOfObjects = new List<Countries>();

            var request = new HttpRequestMessage(HttpMethod.Get, "/v3.1/region/Africa?fields=name");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                listOfObjects = JsonConvert.DeserializeObject<List<Countries>>(await response.Content.ReadAsStringAsync());
             
            }

            List<string> listOfAfrican = listOfObjects.Select(x => x.name.common).ToList();


            return listOfAfrican;
        }
    }
}
 