
![alt text](https://pbs.twimg.com/profile_banners/1450557845359972360/1636418340/1500x500)

## What is African Objects?

African Objects is a bot on [Twitter](https://twitter.com/AfricanObjects/) and [Instagram](https://www.instagram.com/africanobjects/) that posts images of African Art every two hours. Currently being hosted as an Azure function and a timer trigger to send out a post.

## Data

Data is provided by the following API's (thank you for giving me access to the stolen objects) :
- [The Met](https://metmuseum.github.io/)
- [Smithsonian Art Insitute](http://edan.si.edu/openaccess/apidocs/)
- [Harvard Art Museum](https://github.com/harvardartmuseums/api-docs)

Will look to expand this over time. 

## Schedule 

Schedule to send out posts are held in PublishSchedule using [cron](https://en.wikipedia.org/wiki/Cron) syntax 

```c#

[FunctionName("PublishSchedule")]
public async Task Run([TimerTrigger("0 */2 * * *")]TimerInfo myTimer, ILogger log)
{
    log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

    await _tweetservice.StartTweeting();

    await _instagramservice.StartGramming();
}
```

## Running locally

It makes use of environmental variables to store API keys and tokens in Azure this will be stored in the function configuration, but locally it's stored in local.settings.json. 

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
## Songs that got me though this project

![alt text](https://i.imgur.com/NQ0LHU1.png)

## Possible Plans
- Create an African Objects NuGet package to centralise access to all these apis.

