using JokeGenerator.Models;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    interface IUserPromptService
    {
        Task StartInteractionAsync();
        Task<string[]> GetRandomJokesAsync(IPerson person, string category, int number);
        void PrintResults(IConsolePrinterService printer, string[] results);
        void PrintCategoryResults(IConsolePrinterService printer, string[] results);
        Task<string[]> GetCategoriesAsync();
        Task<IPerson> GetRandomPersonAsync();
    }
}
