using JokeGenerator.Models;
using System;

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

        public void StartInteraction()
        {
            char key;
            bool randomPerson = false;
            string[] results = new string[50];            
            printer.PrintValue(Constants.HelpPrompt);
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.PrintNewline();
                    printer.PrintValue(Constants.CategoriesPrompt);
                    printer.PrintValue(Constants.RandomJokesPrompt);
                    key = keyMapper.GetEnteredKey(Console.ReadKey());
                    
                    if (key == 'c')
                    {
                        results = GetCategories();
                        printer.PrintNewline();
                        PrintResults(printer, results);
                    }
                    if (key == 'r')
                    {
                        printer.PrintNewline();
                        printer.PrintValue(Constants.RandomNamesPrompt);
                        key = keyMapper.GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                            randomPerson = true;
                        printer.PrintNewline();

                        printer.PrintValue(Constants.SpecifyCategoryPrompt);
                        // fixed a bug here to capture the category opt in
                        key = keyMapper.GetEnteredKey(Console.ReadKey());
                        printer.PrintNewline();

                        if (key == 'y')
                        {                            
                            printer.PrintValue(Constants.JokesCountPrompt);
                            int n = Int32.Parse(Console.ReadLine());
                            printer.PrintNewline();

                            printer.PrintValue(Constants.EnterCategoryPrompt);
                            // fixed a bug here to capture the category
                            string category = Console.ReadLine();
                            printer.PrintNewline();

                            results = GetRandomJokes(randomPerson ? GetRandomPerson() : null, category, n);
                            PrintResults(printer, results);
                        }
                        else
                        {                            
                            printer.PrintValue(Constants.JokesCountPrompt);
                            int n = Int32.Parse(Console.ReadLine());
                            printer.PrintNewline();

                            results = GetRandomJokes(randomPerson ? GetRandomPerson() : null, null, n);
                            PrintResults(printer, results);
                            printer.PrintNewline();
                        }
                    }
                    randomPerson = false;
                }
            }
        }

        public string[] GetRandomJokes(IPerson person, string category, int numberOfJokes)
        {
            return jsonFeed?.GetRandomJokes(person, category, numberOfJokes);
        }
        public void PrintResults(IConsolePrinterService printer, string[] results)
        {
            //printer?.Value("[" + string.Join(",", results) + "]")?.ToString();
            printer.PrettyPrintResults(results);
        }

        public string[] GetCategories()
        {            
            return jsonFeed?.GetCategories();
        }

        public IPerson GetRandomPerson()
        {
            dynamic result = personFeed.GetNames();
            return new Person(result?.name?.ToString(), result?.surname?.ToString(), result?.gender?.ToString());
        }
    }
}
