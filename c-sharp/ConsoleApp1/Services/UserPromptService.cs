using JokeGenerator.Models;
using System;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public class UserPromptService: IUserPromptService
    {
        readonly IConsolePrinterService printer = null;
        readonly IConsoleKeyMapperService keyMapper = null;
        readonly IJsonFeedService jsonFeed = null;
        readonly IPersonService personFeed = null;

        public UserPromptService(IConsolePrinterService consolePrinterService,
            IConsoleKeyMapperService consoleKeyMapperService,
            IJsonFeedService jsonFeedService,
            IPersonService personService)
        {
            printer = consolePrinterService;
            keyMapper = consoleKeyMapperService;
            jsonFeed = jsonFeedService;
            personFeed = personService;
        }

        public async Task StartInteractionAsync()
        {
            char key;
            bool randomPerson = false;
            string[] results = new string[50];            
            printer.PrintMessage(Constants.HelpPrompt);
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.PrintNewline();
                    printer.PrintMessage(Constants.CategoriesPrompt);
                    printer.PrintMessage(Constants.RandomJokesPrompt);
                    key = keyMapper.GetEnteredKey(Console.ReadKey());
                    
                    if (key == 'c')
                    {
                        results = await GetCategoriesAsync();
                        printer.PrintNewline();
                        PrintCategoryResults(printer, results);
                    }
                    if (key == 'r')
                    {
                        printer.PrintNewline();
                        printer.PrintMessage(Constants.RandomNamesPrompt);
                        key = keyMapper.GetEnteredKey(Console.ReadKey());
                        
                        // yes for random person
                        if (key == 'y')
                            randomPerson = true;
                        printer.PrintNewline();

                        printer.PrintMessage(Constants.SpecifyCategoryPrompt);
                        // fixed a bug here to capture the category opt in
                        key = keyMapper.GetEnteredKey(Console.ReadKey());
                        printer.PrintNewline();

                        // yes for entering category
                        if (key == 'y')
                        {                            
                            printer.PrintMessage(Constants.EnterCategoryPrompt);
                            // fixed a bug here to capture the category
                            string category = Console.ReadLine();
                            printer.PrintNewline();

                            await GetRandomJokesByCount(printer, results, randomPerson, category);                            
                        }
                        else
                        {
                            await GetRandomJokesByCount(printer, results, randomPerson, null);                            
                        }
                    }
                    randomPerson = false;
                }
            }            
        }

        private async Task GetRandomJokesByCount(IConsolePrinterService printer, string[] results, bool randomPerson, string category)
        {
            printer.PrintMessage(Constants.JokesCountPrompt);
            if (Int32.TryParse(Console.ReadLine(), out int n))
            {
                printer.PrintNewline();

                results = await GetRandomJokesAsync(randomPerson ? await GetRandomPersonAsync() : null, category, n);
                PrintResults(printer, results);
            }
            else
            {
                printer.PrintErrorMessage(Constants.ErrorInvalidJokeCount);
            }
        }

        public async Task<string[]> GetRandomJokesAsync(IPerson person, string category, int numberOfJokes)
        {
            return await jsonFeed?.GetRandomJokesAsync(person, category, numberOfJokes);
        }
        public void PrintResults(IConsolePrinterService printer, string[] results)
        {            
            printer.PrettyPrintResults(results);
        }

        public void PrintCategoryResults(IConsolePrinterService printer, string[] results)
        {            
            printer.PrettyPrintCategories(results);
        }

        public async Task<string[]> GetCategoriesAsync()
        {            
            return await jsonFeed?.GetCategoriesAsync();
        }

        public async Task<IPerson> GetRandomPersonAsync()
        {
            dynamic result = await personFeed.GetNamesAsync();
            return new Person(result?.name?.ToString(), result?.surname?.ToString(), result?.gender?.ToString());
        }
    }
}
