using AfricanObjects.Interface;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AfricanObjects
{
    public class PublishSchedule
    {
        ITweetService _tweetservice;
        IInstagramService _instagramservice;    
        public PublishSchedule(ITweetService tweetservice, IInstagramService instagramService)
        {
            _tweetservice = tweetservice;
            _instagramservice = instagramService;
        }

        //[TimerTrigger("0 */2 * * *")]  Post at the 0 minute of every 2 hours
        //[TimerTrigger("*/30 * * * * *")]  Post every 10 seconds

        [FunctionName("PostPublishSchedule")]
        public async Task PostPublishSchedule([TimerTrigger("0 */2 * * *")] TimerInfo myTimer, ILogger log)
        {
            
            await _tweetservice.StartTweeting();

            await _instagramservice.StartGramming();
        }


    }
}
