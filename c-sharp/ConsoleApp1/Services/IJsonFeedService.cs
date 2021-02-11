using JokeGenerator.Models;

namespace JokeGenerator.Services
{
    public interface IJsonFeedService
    {
        string[] GetRandomJokes(IPerson person, string category, int requestedNumOfJokes);        
        string[] GetCategories();
    }
}
