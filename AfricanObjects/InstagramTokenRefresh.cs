using System;
using System.Threading.Tasks;
using AfricanObjects.Interface;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AfricanObjects
{
    public class InstagramTokenRefresh
    {
      
        IInstagramService _instagramservice;
        public InstagramTokenRefresh( IInstagramService instagramService)
        {
       
            _instagramservice = instagramService;
        }

        //[TimerTrigger(""0 0 1 * *")] 
        //[TimerTrigger("*/30 * * * * *")]  Post every 10 seconds
        [FunctionName("InstagramTokenRefresh")]
        public async Task Run([TimerTrigger("0 0 1 * *")]TimerInfo myTimer, ILogger log)
        {
            await _instagramservice.LongLivedToken();
        }
    }
}
