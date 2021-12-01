using AfricanObjects.Interface;
using AfricanObjects.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AfricanObjects.Service
{

    public class TweetService : ITweetService
    {
        private readonly HttpClient client;
        private static readonly string TWITTER_CONSUMER_KEY_SECRET = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_KEY_SECRET");
        private readonly string TWITTER_CONSUMER_API_KEY = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_API_KEY");

        private readonly string TWITTER_ACCESS_TOKEN = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN");
        private static readonly string TWITTER_ACCESS_TOKEN_SECRET = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");

        private readonly HMACSHA1 _sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes($"{TWITTER_CONSUMER_KEY_SECRET}&{TWITTER_ACCESS_TOKEN_SECRET}"));
        private readonly DateTime _epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly string _TwitterImageAPI = "https://upload.twitter.com/1.1/media/upload.json";
        private readonly string _TwitterTextAPI = "https://api.twitter.com/1.1/statuses/update.json";
        private static TwitterResponse uploadResponse = new TwitterResponse();


        private Random rand = new Random();




        private readonly IEnumerable<IMuseumService> museumServices;
        IMetMuseumService metMuseumServivce;

        public TweetService(IHttpClientFactory clientFactory, IEnumerable<IMuseumService> museumServices, IMetMuseumService metMuseumServivce)
        {
            this.client = clientFactory.CreateClient();
            this.museumServices = museumServices;
            this.metMuseumServivce = metMuseumServivce;

        }

        public async Task<TweetObject> StartTweeting()
        {



            TweetObject museumObject = null;
            try
            {

                int rnd = rand.Next(0, 3);

   
                switch (rnd)
                {
                    case 0:

                        do
                        {
                            museumObject = await museumServices.ElementAt(0).GetMuseumObject();

                        } while (museumObject == null);

                        break;

                    case 1:

                        do
                        {
                            museumObject = await museumServices.ElementAt(1).GetMuseumObject();

                        } while (museumObject == null);

                        break;

                    default:

                        do
                        {
                            museumObject = await metMuseumServivce.GetMuseumObject();

                        } while (museumObject == null);

                        break;

                }

                bool response = await UploadImage(museumObject.objectImage);

                if (response)
                {
                    await FomatTweet(museumObject);
                }

            }
            catch (Exception ex)
            {
                return null;

            }

            return museumObject;

        }

        public async Task<TweetObject> FomatTweet(TweetObject museumObject)
        {
            Dictionary<string, string> tweetDictionary = new Dictionary<string, string>();

            tweetDictionary.Add("Title:", museumObject?.Title);
            tweetDictionary.Add("Location:", museumObject?.Location);
            tweetDictionary.Add("Culture:", museumObject?.Culture);
            tweetDictionary.Add("Date:", museumObject?.objectDate);
    
            String tweet = null;
            foreach (var item in tweetDictionary)
            {
                if (!string.IsNullOrEmpty(item.Value) )
                {
                    tweet += item.Key + " " + item.Value + "\n\n";
                }
            }

            var tweetReponse = await TweetText($"{tweet}{museumObject.objectURL} #{museumObject.Country.Replace(" ", string.Empty)} #ArtBot");

            if (tweetReponse)
            {
                return museumObject;
            }

            return null;
        }

        public async Task<bool> UploadImage(String imageurl)
        {

            byte[] bytes = await client.GetByteArrayAsync(imageurl);

            ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);

            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                { byteArrayContent, "media" }
            };

            if (client.DefaultRequestHeaders.Any(x => x.Key == "Authorization"))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
            }

            client.DefaultRequestHeaders.Add("Authorization", PrepareOAuth(_TwitterImageAPI, null, "POST"));

            HttpResponseMessage responseObject = await client.PostAsync("https://upload.twitter.com/1.1/media/upload.json", form);

            string response = await responseObject.Content.ReadAsStringAsync();

            if (responseObject.IsSuccessStatusCode)
            {
                uploadResponse = JsonConvert.DeserializeObject<TwitterResponse>(response);

            }

            return responseObject.IsSuccessStatusCode;
        }

        public async Task<bool> TweetText(string text)
        {
            Dictionary<string, string> textData = new Dictionary<string, string> {
                { "status", text },
                { "trim_user", "1" },
                { "media_ids", uploadResponse.media_id_string}
            };

            return await SendText(_TwitterTextAPI, textData);
        }

        public async Task<bool> SendText(string URL, Dictionary<string, string> textData)
        {


            if (client.DefaultRequestHeaders.Any(x => x.Key == "Authorization"))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
            }

            client.DefaultRequestHeaders.Add("Authorization", PrepareOAuth(URL, textData, "POST"));




            HttpResponseMessage httpResponse = await client.PostAsync(URL, new FormUrlEncodedContent(textData));
            string httpContent = await httpResponse.Content.ReadAsStringAsync();

            return httpResponse.IsSuccessStatusCode;

        }

        public string PrepareOAuth(string URL, Dictionary<string, string> data, string httpMethod)
        {
            // seconds passed since 1/1/1970
            int timestamp = (int)((DateTime.UtcNow - _epochUtc).TotalSeconds);

            // Add all the OAuth headers we'll need to use when constructing the hash
            Dictionary<string, string> oAuthData = new Dictionary<string, string>
            {
                { "oauth_consumer_key", TWITTER_CONSUMER_API_KEY },
                { "oauth_signature_method", "HMAC-SHA1" },
                { "oauth_timestamp", timestamp.ToString() },
                { "oauth_nonce", Guid.NewGuid().ToString() },
                { "oauth_token", TWITTER_ACCESS_TOKEN },
                { "oauth_version", "1.0" }
            };

            if (data != null) // add text data too, because it is a part of the signature
            {
                foreach (KeyValuePair<string, string> item in data)
                {
                    oAuthData.Add(item.Key, item.Value);
                }
            }

            // Generate the OAuth signature and add it to our payload
            oAuthData.Add("oauth_signature", GenerateSignature(URL, oAuthData, httpMethod));

            // Build the OAuth HTTP Header from the data
            return GenerateOAuthHeader(oAuthData);
        }

        public string GenerateSignature(string url, Dictionary<string, string> data, string httpMethod)
        {
            string sigString = string.Join(
                "&",
                data
                    .Union(data)
                    .Select(kvp => string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
                    .OrderBy(s => s)
            );

            string fullSigData = string.Format("{0}&{1}&{2}",
                httpMethod,
                Uri.EscapeDataString(url),
                Uri.EscapeDataString(sigString.ToString()
                )
            );

            return Convert.ToBase64String(
                _sigHasher.ComputeHash(
                    new ASCIIEncoding().GetBytes(fullSigData.ToString())
                )
            );


        }

        public string GenerateOAuthHeader(Dictionary<string, string> data)
        {
            return string.Format(
                "OAuth {0}",
                string.Join(
                    ", ",
                    data
                        .Where(kvp => kvp.Key.StartsWith("oauth_"))
                        .Select(
                            kvp => string.Format("{0}=\"{1}\"",
                            Uri.EscapeDataString(kvp.Key),
                            Uri.EscapeDataString(kvp.Value)
                            )
                        ).OrderBy(s => s)
                    )
                );
        }
    }
}



