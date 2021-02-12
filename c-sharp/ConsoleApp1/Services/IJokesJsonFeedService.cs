using JokeGenerator.Models;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public interface IJokesJsonFeedService
    {
        Task<string[]> GetRandomJokesAsync(IPerson person, string category, int requestedNumOfJokes);        
        Task<string[]> GetCategoriesAsync();
    }
}
