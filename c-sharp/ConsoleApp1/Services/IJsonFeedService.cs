using JokeGenerator.Models;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public interface IJsonFeedService
    {
        Task<string[]> GetRandomJokesAsync(IPerson person, string category, int requestedNumOfJokes);        
        Task<string[]> GetCategoriesAsync();
    }
}
