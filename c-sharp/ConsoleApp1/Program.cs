// removed unused using
using JokeGenerator.Models;
using JokeGenerator.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace JokeGeneratorConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Load configuration from app settings
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var settings = config.GetSection("Settings").Get<Settings>();

            //Setup our DI
            var serviceProvider = new ServiceCollection()                
                .AddSingleton<IConsolePrinterService, ConsolePrinterService>()  
                .AddSingleton<IConsoleKeyMapperService, ConsoleKeyMapperService>()                
                .AddSingleton<IPersonService>(x => new PersonService(settings.RandomPersonAPI))
                .AddSingleton<IJsonFeedService>(j => new JsonFeedService(settings.ChuckNorrisAPI, settings.DefaultNumberOfJokes))
                .AddSingleton<IUserPromptService, UserPromptService>()
                .BuildServiceProvider();
            
            //Kick start user interaction
            IUserPromptService userPrompt = serviceProvider.GetService<IUserPromptService>();
            await userPrompt.StartInteractionAsync();
        }        
    }
}
