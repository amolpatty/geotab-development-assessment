// removed unused using

using JokeGenerator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JokeGeneratorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup our DI
            var serviceProvider = new ServiceCollection()                
                .AddSingleton<IConsolePrinterService, ConsolePrinterService>()  
                .AddSingleton<IConsoleKeyMapperService, ConsoleKeyMapperService>()                
                .AddSingleton<IPersonService>(x => new PersonService("https://www.names.privserv.com/api/"))
                .AddSingleton<IJsonFeedService, JsonFeedService>()
                .AddSingleton<IUserPromptService, UserPromptService>()
                .BuildServiceProvider();

            //Kick start user interaction
            IUserPromptService userPrompt = serviceProvider.GetService<IUserPromptService>();
            userPrompt.StartInteraction();
        }        
    }
}
