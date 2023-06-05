using AfricanObjects.Interface;
using AfricanObjects.Service;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;


[assembly: FunctionsStartup(typeof(AfricanObjects.Startup))]

namespace AfricanObjects
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddTransient<ITweetService, TweetService>();
            builder.Services.AddTransient<IMuseumService, HarvardArtMuseumService>();
            builder.Services.AddTransient<IMuseumService, SmithsonianMuseumService>();
            builder.Services.AddTransient<IMetMuseumService, TheMetMuseumService>();
            builder.Services.AddTransient<ICountryService, CountryService>();
            builder.Services.AddTransient<IInstagramService, InstagramService>();
            builder.Services.AddTransient<IMuseumCollection, MuseumCollection>();

            builder.Services.AddHttpClient("TheMet", c =>
            {
                c.BaseAddress = new Uri("https://collectionapi.metmuseum.org/v1/");
                      
            }).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _=>TimeSpan.FromMilliseconds(300)));

            builder.Services.AddHttpClient("HarvardArtMuseums", c =>
            {
                c.BaseAddress = new Uri("https://api.harvardartmuseums.org/");

            }).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            builder.Services.AddHttpClient("Smithsonian", c =>
            {
                c.BaseAddress = new Uri("https://api.si.edu/openaccess/api/v1.0/");

            }).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));


            builder.Services.AddHttpClient("RestCountries", c =>
            {
                c.BaseAddress = new Uri("https://restcountries.com/");

            }).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));


            builder.Services.AddHttpClient("FacebookGraphAPI", c =>
            {
                c.BaseAddress = new Uri("https://graph.facebook.com/");

            }).AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

        }
    }
}