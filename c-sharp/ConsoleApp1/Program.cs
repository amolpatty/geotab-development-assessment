// removed unused using
using JokeGenerator.Models;
using JokeGenerator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace JokeGeneratorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration from app settings
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var urls = config.GetSection("Url").Get<UrlSettings>();

            //Setup our DI
            var serviceProvider = new ServiceCollection()                
                .AddSingleton<IConsolePrinterService, ConsolePrinterService>()  
                .AddSingleton<IConsoleKeyMapperService, ConsoleKeyMapperService>()                
                .AddSingleton<IPersonService>(x => new PersonService(urls.RandomPersonAPI))
                .AddSingleton<IJsonFeedService>(j => new JsonFeedService(urls.ChuckNorrisAPI, 0))
                .AddSingleton<IUserPromptService, UserPromptService>()
                .BuildServiceProvider();
            
            //Kick start user interaction
            IUserPromptService userPrompt = serviceProvider.GetService<IUserPromptService>();
            userPrompt.StartInteraction();
        }        
    }
}
