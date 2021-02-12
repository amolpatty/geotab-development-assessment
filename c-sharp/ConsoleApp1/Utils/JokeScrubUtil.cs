using JokeGenerator.Models;
using System;

namespace JokeGenerator.Utils
{
    public class JokeScrubUtil : IJokeScrubUtil
    {
        public string ScrubJoke(IPerson person, string joke)
        {
            string scrubbedJoke = Constants.ErrorNoJokeFound;
            if (!string.IsNullOrWhiteSpace(joke))
            {
                scrubbedJoke = joke;
                if (person != null)
                {
                    // Chuck Norris string is immutable (c# joke)
                    // Our only option is to create a copy by replacing his name with a random person
                    if (!string.IsNullOrWhiteSpace(person.Name) && !string.IsNullOrWhiteSpace(person.SurName))
                    {
                        scrubbedJoke = joke.SafeReplace(Constants.ChuckNorris, person.Name + " " + person.SurName, true);
                    }

                    // Try to fix gender specific words
                    if (person.Gender.Equals(Constants.Female, StringComparison.InvariantCultureIgnoreCase))
                    {
                        scrubbedJoke = scrubbedJoke.SafeReplace(Constants.He, Constants.She, true)
                            .SafeReplace(Constants.Him, Constants.Her, true)
                            .SafeReplace(Constants.His, Constants.Her, true)
                            .SafeReplace(Constants.he, Constants.she, true)
                            .SafeReplace(Constants.him, Constants.her, true)
                            .SafeReplace(Constants.his, Constants.her, true)
                            .SafeReplace(Constants.he, Constants.she, true)
                            .SafeReplace(Constants.himeself, Constants.herself, true);
                    }
                }
            }
            return scrubbedJoke;
        }
    }
}
