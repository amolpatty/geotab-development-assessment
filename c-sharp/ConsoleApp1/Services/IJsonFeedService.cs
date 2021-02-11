namespace JokeGenerator.Services
{
    public interface IJsonFeedService
    {
        string[] GetRandomJokes(IPerson person, string category);        
        string[] GetCategories();
    }
}
