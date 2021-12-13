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
        private static readonly string INSTAGRAM_ACCESS_TOKEN = Environment.GetEnvironmentVariable("INSTAGRAM_ACCESS_TOKEN");
      
        public InstagramService(IHttpClientFactory clientFactory, IMuseumCollection museumCollection )
        {
            client = clientFactory.CreateClient("FacebookGraphAPI");
            _museumCollection = museumCollection;

        }

        bool GetTokenInfo()
        {
            return false;
        }

        bool GetLongLivedToken()
        {
            return false;
        }

        public async Task<bool> PostImage(string imageURL, string caption)
        {
            PostImageResponse postImageResponse = new PostImageResponse();

            var request = new HttpRequestMessage(HttpMethod.Post, $"/{INSTAGRAM_USER_ID}/media?access_token={INSTAGRAM_ACCESS_TOKEN}");

            request.Content = JsonContent.Create(new { image_url = imageURL, caption = caption });

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                postImageResponse = JsonConvert.DeserializeObject<PostImageResponse>(await response.Content.ReadAsStringAsync());

                imagePostId = postImageResponse.id;

                return true;
            }

            //Instagram is strict on aspect ratio of image
            return false;
        }

        public async Task<bool> CreatPost(string imageContentId)
        {
            PostImageResponse postImageResponse = new PostImageResponse();

            var request = new HttpRequestMessage(HttpMethod.Post, $"/{INSTAGRAM_USER_ID}/media_publish?access_token={INSTAGRAM_ACCESS_TOKEN}");

            request.Content = JsonContent.Create(new { creation_id = imageContentId });

            HttpResponseMessage response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            //Instagram is strict on aspect ratio of image
            return false;
        }

        public async Task<bool> StartGramming()
        {
            
            bool postImage = false;
            Random rand = new Random();
            try
            {
                MuseumObject museumObject = await _museumCollection.GetMuseumObjectFromCollection();

                do
                {
                    bool response = await PostImage(museumObject.objectImage, String.Format("{0} {1} {2} #{3}",  museumObject.Title, museumObject.objectDate, museumObject.Source, museumObject.Country));

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
