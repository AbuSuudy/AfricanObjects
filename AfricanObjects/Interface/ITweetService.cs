using AfricanObjects.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface ITweetService
    {
        public Task<bool> UploadImage(string imageURL);
        public Task<MuseumObject> StartTweeting();
        public Task<bool> TweetText(string text);
        public Task<bool> SendText(string URL, Dictionary<string, string> textData);
        public string PrepareOAuth(string URL, Dictionary<string, string> data, string httpMethod);
        public string GenerateSignature(string url, Dictionary<string, string> data, string httpMethod);
        public string GenerateOAuthHeader(Dictionary<string, string> data);
    }
}
