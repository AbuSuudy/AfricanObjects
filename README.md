
![alt text](https://user-images.githubusercontent.com/15980314/212194101-d5c7095d-017a-470b-a80a-33b2b3c71c15.jpg)

## What is African Objects?

African Objects is a bot on [Twitter](https://twitter.com/AfricanObjects/) and [Instagram](https://www.instagram.com/africanobjects/) that posts images of African Art. Currently being hosted as an Azure function on a timer trigger to send out a post.

## Data

Data is provided by the following API's :
- [The Met](https://metmuseum.github.io/)
- [Smithsonian Art Insitute](http://edan.si.edu/openaccess/apidocs/)
- [Harvard Art Museum](https://github.com/harvardartmuseums/api-docs)

Will look to expand this over time. 

## Schedule 

Schedule to send out posts are held in PublishSchedule using [cron](https://en.wikipedia.org/wiki/Cron) syntax 

```c#

[FunctionName("PublishSchedule")]
public async Task Run([TimerTrigger("0 */3 * * *")]TimerInfo myTimer, ILogger log)
{

    await _tweetservice.StartTweeting();

    await _instagramservice.StartGramming();
}
```

## Running locally

It makes use of environmental variables to store API keys and tokens in Azure this will be stored in the function configuration, but locally it's stored in local.settings.json. You would need to register with these platforms and museums to get keys to use their service.

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "HARVARD_API_KEY": "",
        "SMITHSONIAN_API_KEY":"",
        "TWITTER_CONSUMER_KEY_SECRET":"",
        "TWITTER_CONSUMER_API_KEY":"",
        "TWITTER_ACCESS_TOKEN":"",
        "TWITTER_ACCESS_TOKEN_SECRET":"",
	"INSTAGRAM_USER_ID": "",
	"INSTAGRAM_CLIENT_ID": "",
  	"INSTAGRAM_CLIENT_SECRET": "",
	"INSTAGRAM_ACCESS_TOKEN": ""
    }
}
```

