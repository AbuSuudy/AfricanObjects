using AfricanObjects.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface ITweetService
    {
        public Task<bool> UploadImage(string imageURL, CancellationToken token);
        public Task<MuseumObject> StartTweeting(CancellationToken token);
        public Task<bool> TweetText(string text, CancellationToken token);
        public Task<bool> SendText(string URL, MediaTweet media, CancellationToken token);
        public string PrepareOAuth(string URL, Dictionary<string, string> data, string httpMethod, CancellationToken token);
        public string GenerateSignature(string url, Dictionary<string, string> data, string httpMethod, CancellationToken token);
        public string GenerateOAuthHeader(Dictionary<string, string> data, CancellationToken token);
    }
}
