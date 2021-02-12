using JokeGenerator.Models;

namespace JokeGenerator.Utils
{
    public interface IJokeScrubUtil
    {
        string ScrubJoke(IPerson person, string joke);
    }
}