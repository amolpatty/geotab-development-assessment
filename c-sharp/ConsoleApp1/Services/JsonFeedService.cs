using System;
using System.Net.Http;
using System.Threading.Tasks;
using JokeGenerator.Models;
using Newtonsoft.Json;

namespace JokeGenerator.Services
{
    public class JsonFeedService: IJsonFeedService
    {
        readonly string _url;
        readonly int _numberOfJokes = 1;

		// Recommended pattern as per this article
		// https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
		private static readonly HttpClient client = new HttpClient();

		public JsonFeedService() { }
        public JsonFeedService(string endpoint, int numberOfJokes)
        {
            _url = endpoint;
			client.BaseAddress = new Uri(_url);
			_numberOfJokes = numberOfJokes;
        }
        
		public string[] GetRandomJokes(IPerson person, string category, int requestedNumOfJokes)
		{
			string[] jokes = new string[requestedNumOfJokes];
			// todo: use using
			// validate category against actual categories

			int jokeCount = 0;
						
			string url = "jokes/random";
			if (category != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "category=";
				url += category;
			}

			while (jokeCount < requestedNumOfJokes)
			{
				string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

				if (person?.FirstName != null && person?.LastName != null)
				{
					joke = joke.Replace(Constants.ChuckNorris, person?.FirstName + " " + person?.LastName);
				}
								
				jokes[jokeCount] = JsonConvert.DeserializeObject<dynamic>(joke).value;
				
				jokeCount++;
			}
			
			return jokes;
        }

		public string[] GetCategories()
		{	
			// fixed the url here
			return new string[] { Task.FromResult(client.GetStringAsync("jokes/categories").Result).Result };
		}
    }
}
