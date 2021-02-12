// removed unused using
using JokeGenerator;
using JokeGenerator.Models;
using JokeGenerator.Services;
using JokeGenerator.Utils;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JokeGeneratorConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Load configuration from app settings
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
                var config = builder.Build();
                var settings = config.GetSection("Settings").Get<Settings>();

                // Setup our DI
                var serviceProvider = new ServiceCollection()           
                    .AddMemoryCache()
                    .AddSingleton<IConsolePrinterService, ConsolePrinterService>()
                    .AddSingleton<IConsoleKeyMapperService, ConsoleKeyMapperService>()
                    .AddSingleton<IJokeScrubUtil, JokeScrubUtil>()
                    .AddSingleton<IPersonService>(x => new PersonService(settings.RandomPersonAPI))
                    .AddSingleton<IJokesJsonFeedService>(j => new JokesJsonFeedService(j.GetService<IJokeScrubUtil>(), settings.ChuckNorrisAPI, settings.DefaultNumberOfJokes))
                    .AddSingleton<IUserPromptService, UserPromptService>()                    
                    .BuildServiceProvider();

                // Kick start user interaction
                IUserPromptService userPrompt = serviceProvider.GetService<IUserPromptService>();
                await userPrompt.StartInteractionAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(Environment.NewLine + Constants.ErrorSystemFault + " " + Constants.ErrorDetails + " " + ex?.Message);
            }
        }
    }
}
