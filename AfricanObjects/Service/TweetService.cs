﻿using AfricanObjects.Interface;
using AfricanObjects.Models;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

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
        private readonly string _TwitterTextAPI = "https://api.twitter.com/2/tweets";
        private static List<string> ImageIDs = new List<string>();
        private List<string> imagesId = new List<string>();
        private IMuseumCollection museumCollection;

        public TweetService(IHttpClientFactory clientFactory, IMuseumCollection museumCollection)
        {
            this.client = clientFactory.CreateClient();
            this.museumCollection = museumCollection;


        }

        public async Task<MuseumObject> StartTweeting(CancellationToken token)
        {
            try
            {
                ImageIDs.Clear();

                MuseumObject museumObject = await museumCollection.GetMuseumObjectFromCollection();

                foreach (var item in museumObject.objectImage)
                {
                    await UploadImage(item, token);
                }

                await FomatTweet(museumObject, token);


                return museumObject;

            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public async Task<MuseumObject> FomatTweet(MuseumObject museumObject, CancellationToken token)
        {
            Dictionary<string, string> tweetDictionary = new Dictionary<string, string>();

            tweetDictionary.Add("Title:", museumObject?.Title);
            tweetDictionary.Add("Location:", museumObject?.Location);
            tweetDictionary.Add("Culture:", museumObject?.Culture);
            tweetDictionary.Add("Date:", museumObject?.objectDate);

            String tweet = null;
            foreach (var item in tweetDictionary)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    tweet += item.Key + " " + item.Value + "\n\n";
                }
            }

            var tweetReponse = await TweetText($"{tweet}{museumObject.objectURL} \n\n #{Regex.Replace(museumObject.Country, @"[^\w]", string.Empty)} #ArtBot", token);

            if (tweetReponse)
            {
                return museumObject;
            }

            return null;
        }

        public async Task<bool> UploadImage(String imageurl, CancellationToken token)
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

            client.DefaultRequestHeaders.Add("Authorization", PrepareOAuth(_TwitterImageAPI, null, "POST", token));

            HttpResponseMessage responseObject = await client.PostAsync("https://upload.twitter.com/1.1/media/upload.json", form);

            string response = await responseObject.Content.ReadAsStringAsync();

            if (responseObject.IsSuccessStatusCode)
            {
                TwitterResponse uploadResponse = JsonSerializer.Deserialize<TwitterResponse>(response);
                ImageIDs.Add(uploadResponse.media_id_string);

            }

            return responseObject.IsSuccessStatusCode;
        }

        public async Task<bool> TweetText(string text, CancellationToken token)
        {
          

            MediaTweet mediaTweet = new MediaTweet
            {
                text = text,

                media = new Media
                {
                    media_ids = ImageIDs
                }
            };
            

            return await SendText(_TwitterTextAPI, mediaTweet, token);
        }

        public async Task<bool> SendText(string URL, MediaTweet media, CancellationToken token)
        {


            if (client.DefaultRequestHeaders.Any(x => x.Key == "Authorization"))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
            }

            Dictionary<string, string> textData = new Dictionary<string, string> {
                { "text", media.text },
                { "media_ids",string.Join(",", ImageIDs)}
            };

            client.DefaultRequestHeaders.Add("Authorization", PrepareOAuth(URL, textData, "POST", token));


            HttpResponseMessage httpResponse = await client.PostAsJsonAsync(URL, media);

            return httpResponse.IsSuccessStatusCode;

        }

        public string PrepareOAuth(string URL, Dictionary<string, string> data, string httpMethod, CancellationToken token)
        {
            // seconds passed since 1/1/1970
            int timestamp = (int)((DateTime.UtcNow - _epochUtc).TotalSeconds);

            // Add all the OAuth headers we'll need to use when constructing the hash
            Dictionary<string, string> oAuthData = new Dictionary<string, string>
            {
                { "oauth_consumer_key", TWITTER_CONSUMER_API_KEY },
                { "oauth_token", TWITTER_ACCESS_TOKEN },
                { "oauth_signature_method", "HMAC-SHA1" },
                { "oauth_timestamp", timestamp.ToString() },
                { "oauth_nonce", Guid.NewGuid().ToString() },
                { "oauth_version", "1.0" }
            };

            // Generate the OAuth signature and add it to our payload
            oAuthData.Add("oauth_signature", GenerateSignature(URL, oAuthData, httpMethod, token));

            // Build the OAuth HTTP Header from the data
            return GenerateOAuthHeader(oAuthData, token);
        }

        public string GenerateSignature(string url, Dictionary<string, string> data, string httpMethod, CancellationToken token)
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

        public string GenerateOAuthHeader(Dictionary<string, string> data, CancellationToken token)
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



