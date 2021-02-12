using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JokeGenerator.Models;
using JokeGenerator.Utils;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace JokeGenerator.Services
{
    /// <summary>
    /// The jokes json feed service provides methods to get random jokes and categories
    /// </summary>
    public class JokesJsonFeedService : IJokesJsonFeedService
    {
        readonly string _url;
        readonly int _defaultNumberOfJokes = 1;

        // Recommended pattern as per this article
        // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        // however this makes it difficult to unit test
        private static readonly HttpClient client = new HttpClient();

        public JokesJsonFeedService() { }
        public JokesJsonFeedService(string endpoint, int numberOfJokes)
        {
            _url = endpoint;
            client.BaseAddress = new Uri(_url);
            _defaultNumberOfJokes = numberOfJokes;
        }

        public async Task<string[]> GetRandomJokesAsync(IPerson person, string category, int requestedNumOfJokes)
        {
            // if the user supplied  number of jokes is not valid then we will use the default number from appsettings
            requestedNumOfJokes = requestedNumOfJokes > 0 ? requestedNumOfJokes : _defaultNumberOfJokes;

            string[] jokes = new string[requestedNumOfJokes];

            string url = Constants.RandomJokesApiURL;
            if (!string.IsNullOrWhiteSpace(category))
            {
                string[] categories = await GetCategoriesAsync();

                // return an error if no categories could be fetched
                if (categories?.Length <= 0)
                {
                    return new string[] { Constants.ErrorNoCategoriesFound };
                }

                // convert the string array to a List so that we can use Contains.
                List<string> categoryList = new List<string>(categories);

                // check if the requested category is one of the valid categories
                if (categoryList.Contains(category))
                {
                    // append the category=movie query string to the url
                    var parametersToAdd = new Dictionary<string, string> { { Constants.CategoryQueryString, category } };
                    url = QueryHelpers.AddQueryString(url, parametersToAdd);
                }
                else
                {
                    // return error if an invalid category was entered
                    return new string[] { Constants.ErrorInvalidCategories };
                }
            }

            int jokeCount = 0;
            // get the jokes 
            // todo change to task.whenall
            while (jokeCount < requestedNumOfJokes)
            {
                string joke = await client.GetStringAsync(url);

                if (!string.IsNullOrWhiteSpace(joke))
                {
                    if (person != null)
                    {
                        // Chuck Norris string is immutable (c# joke)
                        // Our only option is to create a copy by replacing his name with a random person
                        if (!string.IsNullOrWhiteSpace(person.Name) && !string.IsNullOrWhiteSpace(person.SurName))
                        {
                            joke = joke.SafeReplace(Constants.ChuckNorris, person.Name + " " + person.SurName, true);
                        }

                        // Try to fix gender specific words
                        if (person.Gender.Equals(Constants.Female, StringComparison.InvariantCultureIgnoreCase))
                        {
                            joke = joke.SafeReplace(Constants.He, Constants.She, true)
                                .SafeReplace(Constants.Him, Constants.Her, true)
                                .SafeReplace(Constants.His, Constants.Her, true)
                                .SafeReplace(Constants.he, Constants.she, true);
                        }
                    }
                }
                else
                {
                    joke = Constants.ErrorNoJokeFound;
                }

                // Deserialize the json string to a joke object if we want to use the object for some additional processing
                // it would be nice if we can use System.Text.Json here.
                // todo change to strongly typed joke
                jokes[jokeCount] = JsonConvert.DeserializeObject<dynamic>(joke).value;
                
                // explicitly increment counter. This can be done above as well jokes[jokeCount++]
                jokeCount++;
            }

            return jokes;
        }

        public async Task<string[]> GetCategoriesAsync()
        {
            // fixed the url bug here
            string categories = await client.GetStringAsync(Constants.JokesCategoriesApiURL);
            if (!string.IsNullOrWhiteSpace(categories))
            {
                // return a scrubbed string array for prettier output
                return categories.Replace("[", "").Replace("]", "").Replace("\"", "").Split(",");
            }
            else
            {
                return new string[] { Constants.ErrorNoCategoriesFound };
            }
        }
    }
}
