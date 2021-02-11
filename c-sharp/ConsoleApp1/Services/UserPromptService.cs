using System;

namespace JokeGenerator.Services
{
    public class UserPromptService: IUserPromptService
    {   
        IConsolePrinterService printer = null;
        IConsoleKeyMapperService keyMapper = null;
        IJsonFeedService jsonFeed = null;
        IPersonService personFeed = null;

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
            printer.Value(Constants.HelpPrompt).ToString();
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.Value(Constants.CategoriesPrompt).ToString();
                    printer.Value(Constants.RandomJokesPrompt).ToString();
                    key = keyMapper.GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        results = GetCategories();
                        PrintResults(printer, results);
                    }
                    if (key == 'r')
                    {
                        printer.Value(Constants.RandomNamesPrompt).ToString();
                        key = keyMapper.GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                            randomPerson = true;
                        printer.Value(Constants.SpecifyCategoryPrompt).ToString();
                        // fixed a bug here to capture the category response
                        key = keyMapper.GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {
                            printer.Value(Constants.JokesCountPrompt).ToString();
                            int n = Int32.Parse(Console.ReadLine());
                            printer.Value(Constants.EnterCategoryPrompt).ToString();
                            results = GetRandomJokes(randomPerson ? GetRandomPerson() : null, Console.ReadLine(), n);
                            PrintResults(printer, results);
                        }
                        else
                        {
                            printer.Value(Constants.JokesCountPrompt).ToString();
                            int n = Int32.Parse(Console.ReadLine());
                            results = GetRandomJokes(randomPerson ? GetRandomPerson() : null, null, n);
                            PrintResults(printer, results);
                        }
                    }
                    randomPerson = false;
                }
            }
        }

        public string[] GetRandomJokes(IPerson person, string category, int number)
        {
            return new JsonFeedService("https://api.chucknorris.io", number).GetRandomJokes(person, category);
        }
        public void PrintResults(IConsolePrinterService printer, string[] results)
        {
            printer.Value("[" + string.Join(",", results) + "]").ToString();
        }

        public string[] GetCategories()
        {
            return new JsonFeedService("https://api.chucknorris.io", 0).GetCategories();
        }

        public IPerson GetRandomPerson()
        {
            dynamic result = personFeed.GetNames();
            return new Person(result.name.ToString(), result.surname.ToString(), result.gender.ToString());
        }
    }
}
