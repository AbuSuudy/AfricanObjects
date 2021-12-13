using AfricanObjects.Interface;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AfricanObjects
{
    public class TweetSchedule
    {
        ITweetService _tweetservice;
        IInstagramService _instagramservice;    
        public TweetSchedule(ITweetService tweetservice, IInstagramService instagramService)
        {
            _tweetservice = tweetservice;
            _instagramservice = instagramService;
        }

        //[TimerTrigger("0 */2 * * *")]  Post at the 0 minute of every 2 hours
        //[TimerTrigger("*/30 * * * * *")]  Post every 10 seconds

        [FunctionName("TweetSchedule")]
        public async Task Run([TimerTrigger("0 */2 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            await _tweetservice.StartTweeting();

            await _instagramservice.StartGramming();
        }

    }
}
