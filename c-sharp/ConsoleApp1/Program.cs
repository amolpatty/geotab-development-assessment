// removed unused using
using System;
using JokeGenerator;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50];
        static char key;        
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            bool randomPerson = false;
            printer.Value(Constants.HelpPrompt).ToString();
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.Value(Constants.CategoriesPrompt).ToString();
                    printer.Value(Constants.RandomJokesPrompt).ToString();
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        GetCategories();
                        PrintResults();
                    }
                    if (key == 'r')
                    {
                        printer.Value(Constants.RandomNamesPrompt).ToString();
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                            randomPerson = true;
                        printer.Value(Constants.SpecifyCategoryPrompt).ToString();
                        if (key == 'y')
                        {
                            printer.Value(Constants.JokesCountPrompt).ToString();
                            int n = Int32.Parse(Console.ReadLine());
                            printer.Value(Constants.EnterCategoryPrompt).ToString();
                            GetRandomJokes(randomPerson? GetRandomPerson(): null, Console.ReadLine(), n);
                            PrintResults();
                        }
                        else
                        {
                            printer.Value(Constants.JokesCountPrompt).ToString();
                            int n = Int32.Parse(Console.ReadLine());
                            GetRandomJokes(randomPerson? GetRandomPerson(): null, null, n);
                            PrintResults();
                        }
                    }
                    randomPerson = false;
                }
            }

        }

        private static void PrintResults()
        {
            printer.Value("[" + string.Join(",", results) + "]").ToString();
        }

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
            }
        }

        private static void GetRandomJokes(IPerson person, string category, int number)
        {
            new JsonFeed("https://api.chucknorris.io", number);
            results = JsonFeed.GetRandomJokes(person, category);
        }

        private static void GetCategories()
        {
            new JsonFeed("https://api.chucknorris.io", 0);
            results = JsonFeed.GetCategories();
        }

        private static IPerson GetRandomPerson()
        {
            new JsonFeed("https://www.names.privserv.com/api/", 0);
            dynamic result = JsonFeed.GetNames();
            return new Person(result.name.ToString(), result.surname.ToString(), result.gender.ToString());
        }
    }
}
