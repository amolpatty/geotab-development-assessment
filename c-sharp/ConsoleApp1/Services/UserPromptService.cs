using JokeGenerator.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public class UserPromptService: IUserPromptService
    {
        readonly IConsolePrinterService printer = null;
        readonly IConsoleKeyMapperService keyMapper = null;
        readonly IJokesJsonFeedService jsonFeed = null;
        readonly IPersonService personFeed = null;
        readonly IMemoryCache _cache;

        public UserPromptService(IConsolePrinterService consolePrinterService,
            IConsoleKeyMapperService consoleKeyMapperService,
            IJokesJsonFeedService jsonFeedService,
            IPersonService personService,
            IMemoryCache cache
            )
        {
            printer = consolePrinterService;
            keyMapper = consoleKeyMapperService;
            jsonFeed = jsonFeedService;
            personFeed = personService;
            _cache = cache;
        }

        public async Task StartInteractionAsync()
        {
            char key;
            bool randomPerson = false;

            // welcome prompt
            printer.PrintMessage(Constants.ScreenSeparator);
            printer.PrintMessage(Constants.WelcomePrompt);
            printer.PrintMessage(Constants.ScreenSeparator);
            printer.PrintMessage(Constants.InstructionPrompt);
            
            while (true)
            {

                // start with instructions 
                printer.PrintNewline();
                printer.PrintMessage(Constants.ScreenSeparator);
                printer.PrintMessage(Constants.CategoriesPrompt);
                printer.PrintMessage(Constants.RandomJokesPrompt);
                printer.PrintMessage(Constants.QuitPrompt);                
                key = keyMapper.GetEnteredKey(Console.ReadKey());

                if (key == 'q')
                {
                    // exit the loop
                    break;
                }
                else if (key == 'c')
                {
                    string[] results = await GetCategoriesAsync();
                    printer.PrintNewline();
                    printer.PrintMessage(Constants.ScreenSeparator);
                    printer.PrintMessage(Constants.CategoriesTitle);
                    PrintCategoryResults(printer, results);
                }
                else if (key == 'r')
                {
                    // prompt to use random names
                    printer.PrintNewline();
                    printer.PrintMessage(Constants.ScreenSeparator);
                    printer.PrintMessage(Constants.RandomNamesPrompt);
                    key = keyMapper.GetEnteredKey(Console.ReadKey());

                    // yes for random person
                    if (key == 'y')
                        randomPerson = true;
                    
                    // specify category prompt
                    printer.PrintNewline();
                    printer.PrintMessage(Constants.ScreenSeparator);
                    printer.PrintMessage(Constants.SpecifyCategoryPrompt);
                    // fixed a bug here to capture the category opt in
                    key = keyMapper.GetEnteredKey(Console.ReadKey());
                    printer.PrintNewline();
                    printer.PrintMessage(Constants.ScreenSeparator);

                    // yes for entering category
                    if (key == 'y')
                    {                        
                        // enter category prompt
                        printer.PrintMessage(Constants.EnterCategoryPrompt);
                        // fixed a bug here to capture the category
                        string category = Console.ReadLine();
                        printer.PrintNewline();
                        printer.PrintMessage(Constants.ScreenSeparator);
                        
                        // get random jokes by specified category
                        await GetRandomJokesByCount(printer, randomPerson, category);
                    }
                    else
                    {
                        // get random joke by random category
                        await GetRandomJokesByCount(printer, randomPerson, null);
                    }
                }
                randomPerson = false;
            }
        }

        private async Task GetRandomJokesByCount(IConsolePrinterService printer, bool randomPerson, string category)
        {
            printer.PrintMessage(Constants.JokesCountPrompt);
            char key = keyMapper.GetEnteredKey(Console.ReadKey()); 
            
            // check if the joke count is a valid integer
            if (Int32.TryParse(key.ToString(), out int n))
            {
                printer.PrintNewline();                
                string[] results = await GetRandomJokesAsync(randomPerson ? await GetRandomPersonAsync() : null, category, n);
                PrintResults(printer, results);
            }
            else
            {
                printer.PrintNewline();                
                printer.PrintErrorMessage(Constants.ErrorInvalidJokeCount);
            }
        }

        public async Task<string[]> GetRandomJokesAsync(IPerson person, string category, int numberOfJokes)
        {
            // get random jokes using json feed service
            return await jsonFeed?.GetRandomJokesAsync(person, category, numberOfJokes);
        }
        public void PrintResults(IConsolePrinterService printer, string[] results)
        {            
            // display jokes
            printer.PrettyPrintResults(results);
        }

        public void PrintCategoryResults(IConsolePrinterService printer, string[] results)
        {            
            // display categories
            printer.PrettyPrintCategories(results);
        }

        public async Task<string[]> GetCategoriesAsync()
        {
            // Look for cache key.
            if (!_cache.TryGetValue(Constants.CategoriesCacheKey, out string[] cacheEntry))
            {
                // Key not in cache, so get categories using json feed service
                cacheEntry = await jsonFeed?.GetCategoriesAsync(); 

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(3600));

                // Save data in cache.
                _cache.Set(Constants.CategoriesCacheKey, cacheEntry, cacheEntryOptions);
            }
                        
            return cacheEntry;
        }

        public async Task<IPerson> GetRandomPersonAsync()
        {            
            return await personFeed?.GetNamesAsync();            
        }
    }
}
