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

        //Every month// 0 0 1 * *
        //Every Two Days //0 0 2 * *
        [FunctionName("InstagramTokenRefresh")]
        public async Task Run([TimerTrigger("0 0 2 * *")]TimerInfo myTimer, ILogger log)
        {
            await _instagramservice.LongLivedToken();
        }
    }
}
