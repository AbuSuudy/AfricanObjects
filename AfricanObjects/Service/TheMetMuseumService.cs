using AfricanObjects.Interface;
using AfricanObjects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{
    public class TheMetMuseumService : IMetMuseumService
    {
        private HttpClient client;
        private static Random rnd = new Random();
        private static List<string> countries;
        private  ICountryService countryService;

        public TheMetMuseumService(IHttpClientFactory clientFactory, ICountryService countryService)
        {
            client = clientFactory.CreateClient("TheMet");
            this.countryService = countryService;
        }

        public async Task<int> GetObjectId()
        {
            TheMetObjectInDepartment objIds = new TheMetObjectInDepartment();

            var request = new HttpRequestMessage(HttpMethod.Get, "/public/collection/v1/objects?departmentIds=5");

            HttpResponseMessage responseObject = await client.SendAsync(request);

            if (responseObject.IsSuccessStatusCode)
            {
                objIds = JsonConvert.DeserializeObject<TheMetObjectInDepartment>(await responseObject.Content.ReadAsStringAsync());


                objIds.objectIDs.Shuffle();

                return objIds.objectIDs.FirstOrDefault();
            }
            return 0;
        }

        public async Task<MuseumObject> GetMuseumObject()
        {

            MuseumObject museumObject = new MuseumObject();

            try
            {            
                int objectId =await GetObjectId();

                var request = new HttpRequestMessage(HttpMethod.Get, string.Format($"/public/collection/v1/objects/{objectId}"));

                HttpResponseMessage responseObject = await client.SendAsync(request);

                if (responseObject.IsSuccessStatusCode)
                {
                    TheMetMuseumObject theMetObj = JsonConvert.DeserializeObject<TheMetMuseumObject>(await responseObject.Content.ReadAsStringAsync());

                    if (countries == null)
                    {
                        countries = await countryService.GetAllAfricanCountries();
                    }

                    if (!countries.Contains(theMetObj.country) ||  String.IsNullOrEmpty(theMetObj.primaryImage))
                    {
                      
                         return null;

                    }

                    museumObject.Title = theMetObj.title;
                    museumObject.Culture = theMetObj.culture;
                    museumObject.Location = theMetObj.country;
                    museumObject.objectDate = theMetObj.objectDate;
                    museumObject.objectURL = theMetObj.objectURL;
                    museumObject.objectImage = theMetObj.primaryImage;
                    museumObject.Country = theMetObj.country;
                    museumObject.Source = "The Met";



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
