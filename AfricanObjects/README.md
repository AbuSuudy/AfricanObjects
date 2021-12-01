
![alt text](https://pbs.twimg.com/profile_banners/1450557845359972360/1636418340/1500x500)

## What is African Objects?

[African Objects](https://twitter.com/AfricanObjects/) is a Twitter bot that posts images of African Art every two hours. Currently being hosted as an Azure function and a timer trigger to send out a tweet.

## APIs

API's used in this project (thank you for giving me access to the stolen objects) :
- [The Met](https://metmuseum.github.io/)
- [Smithsonian Art Insitute](http://edan.si.edu/openaccess/apidocs/)
- [Harvard Art Museum](https://github.com/harvardartmuseums/api-docs)

Will look to expand this over time. 

## Set up

It makes use of environmental variables to store API keys and tokens in Azure this will be stored in the function configuration, but locally it's stored in local.settings.json

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
        "TWITTER_ACCESS_TOKEN_SECRET":""
    }
}
```

## Possible Plans
- Post images to Instagram because it would seem to be more of a natural fit.
- Create an African Objects NuGet package to centralise access to all these apis.


## License
[MIT](https://choosealicense.com/licenses/mit/)