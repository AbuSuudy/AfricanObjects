using AfricanObjects.Interface;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AfricanObjects
{
    public class PublishSchedule
    {
        private ITweetService _tweetservice;
        private IInstagramService _instagramservice;

        private static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public PublishSchedule(ITweetService tweetservice, IInstagramService instagramService)
        {
            _tweetservice = tweetservice;
            _instagramservice = instagramService;
        }

        //https://arminreiter.com/2017/02/azure-functions-time-trigger-cron-cheat-sheet/
        //[TimerTrigger("0 0 * * * *)]  Post at the 0 minute of every hour
        //[TimerTrigger("*/2 * * * * *")]  Post every 2 seconds
        [FunctionName("PostPublishSchedule")]
        public async Task PostPublishSchedule([TimerTrigger("0 0 * * * *")] TimerInfo myTimer, ILogger log)
        {
            cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(3));

            CancellationToken token = cancellationTokenSource.Token;

            //await _instagramservice.LongLivedToken();

            await _tweetservice.StartTweeting(token);
            
            await _instagramservice.StartGramming(token);

            
        }


    }
}
