using JokeGenerator.Models;

namespace JokeGenerator.Services
{
    interface IUserPromptService
    {
        void StartInteraction();
        string[] GetRandomJokes(IPerson person, string category, int number);
        void PrintResults(IConsolePrinterService printer, string[] results);
        string[] GetCategories();
        IPerson GetRandomPerson();
    }
}
