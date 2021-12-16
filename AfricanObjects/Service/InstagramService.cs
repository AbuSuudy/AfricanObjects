using AfricanObjects.Interface;
using AfricanObjects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{
    public class InstagramService : IInstagramService
    {
        private HttpClient client;
        private string imagePostId;
        private IMuseumCollection _museumCollection;
        private static readonly string INSTAGRAM_USER_ID = Environment.GetEnvironmentVariable("INSTAGRAM_USER_ID");
        private static string INSTAGRAM_ACCESS_TOKEN = Environment.GetEnvironmentVariable("INSTAGRAM_ACCESS_TOKEN");
        private static readonly string INSTAGRAM_CLIENT_ID = Environment.GetEnvironmentVariable("INSTAGRAM_CLIENT_ID");
        private static readonly string INSTAGRAM_CLIENT_SECRET = Environment.GetEnvironmentVariable("INSTAGRAM_CLIENT_SECRET");

        public InstagramService(IHttpClientFactory clientFactory, IMuseumCollection museumCollection )
        {
            client = clientFactory.CreateClient("FacebookGraphAPI");
            _museumCollection = museumCollection;

        }

        public async Task<bool> LongLivedToken()
        {
            LongLivedTokenResponse longLivedTokenResponse = new LongLivedTokenResponse();
            var request = new HttpRequestMessage(HttpMethod.Post, $"/v12.0/oauth/access_token?grant_type=fb_exchange_token&client_id={INSTAGRAM_CLIENT_ID}&client_secret={INSTAGRAM_CLIENT_SECRET}&fb_exchange_token={INSTAGRAM_ACCESS_TOKEN}");


            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                longLivedTokenResponse = JsonConvert.DeserializeObject<LongLivedTokenResponse>(await response.Content.ReadAsStringAsync());

                Environment.SetEnvironmentVariable("INSTAGRAM_ACCESS_TOKEN", longLivedTokenResponse.access_token);
                INSTAGRAM_ACCESS_TOKEN = Environment.GetEnvironmentVariable("INSTAGRAM_ACCESS_TOKEN");

                
            }


            return response.IsSuccessStatusCode; 
        }

        public async Task<bool> PostImage(string imageURL, string caption)
        {

            if (Environment.GetEnvironmentVariable("INSTAGRAM_ACCESS_TOKEN") == null)
            {
                await LongLivedToken();
            }

            PostImageResponse postImageResponse = new PostImageResponse();

            var request = new HttpRequestMessage(HttpMethod.Post, $"/{INSTAGRAM_USER_ID}/media?access_token={INSTAGRAM_ACCESS_TOKEN}");

            request.Content = JsonContent.Create(new { image_url = imageURL, caption = caption });

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                postImageResponse = JsonConvert.DeserializeObject<PostImageResponse>(await response.Content.ReadAsStringAsync());

                imagePostId = postImageResponse.id;

         
            }

            //Instagram is strict on aspect ratio of image so could return false
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreatPost(string imageContentId)
        {
            PostImageResponse postImageResponse = new PostImageResponse();

            var request = new HttpRequestMessage(HttpMethod.Post, $"/{INSTAGRAM_USER_ID}/media_publish?access_token={INSTAGRAM_ACCESS_TOKEN}");

            request.Content = JsonContent.Create(new { creation_id = imageContentId });

            HttpResponseMessage response = await client.SendAsync(request);


            //Instagram is strict on aspect ratio of image
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> StartGramming()
        {
            
            bool postImage = false;

            try
            {
                
                do
                {
                    MuseumObject museumObject = await _museumCollection.GetMuseumObjectFromCollection();

                    bool response = await PostImage(museumObject.objectImage, String.Format("{0} {1} {2} #{3}",  museumObject.Title, museumObject.objectDate, museumObject.Source, string.Concat(museumObject.Country.Where(c => !char.IsWhiteSpace(c)))));

                    if (response)
                    {
                         postImage =  await CreatPost(imagePostId);
                    }

                } while (postImage != true);


            }
            catch (Exception ex)
            {
                return false;

            }

            return postImage;
        }      
    }
}
