using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JokeGenerator.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace JokeGenerator.Services
{
    public class JsonFeedService: IJsonFeedService
    {
        readonly string _url;
        readonly int _defaultNumberOfJokes = 1;

		// Recommended pattern as per this article
		// https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
		private static readonly HttpClient client = new HttpClient();

		public JsonFeedService() { }
        public JsonFeedService(string endpoint, int numberOfJokes)
        {
            _url = endpoint;
			client.BaseAddress = new Uri(_url);
			_defaultNumberOfJokes = numberOfJokes;
        }
        
		public async Task<string[]> GetRandomJokesAsync(IPerson person, string category, int requestedNumOfJokes)
		{
			requestedNumOfJokes = requestedNumOfJokes > 0 ? requestedNumOfJokes : _defaultNumberOfJokes;

			string[] jokes = new string[requestedNumOfJokes];
			// todo: use using
			// validate category against actual categories

			int jokeCount = 0;

			string url = "jokes/random";
			if (!string.IsNullOrWhiteSpace(category))
			{
				string[] categories = await GetCategoriesAsync();
				if (categories?.Length <= 0)
                {
					return new string[] { Constants.ErrorNoCategoriesFound };
                }

				List<string> categoryList = new List<string>(categories);
				if (categoryList.Contains(category))
				{
					var parametersToAdd = new Dictionary<string, string> { { "category", category } };
					url = QueryHelpers.AddQueryString(url, parametersToAdd);
				}
				else
                {
					return new string[] { Constants.ErrorInvalidCategories };
                }
			}

			/*
			if (category != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "category=";
				url += category;
			}
			*/

			while (jokeCount < requestedNumOfJokes)
			{
				string joke = await client.GetStringAsync(url);

				if (person?.FirstName != null && person?.LastName != null)
				{
					joke = joke.Replace(Constants.ChuckNorris, person?.FirstName + " " + person?.LastName);
				}
								
				jokes[jokeCount] = JsonConvert.DeserializeObject<dynamic>(joke).value;
				
				jokeCount++;
			}
			
			return jokes;
        }

		public async Task<string[]> GetCategoriesAsync()
		{
			// fixed the url here
			string categories = await client.GetStringAsync("jokes/categories");
			if (!string.IsNullOrWhiteSpace(categories))
			{
				return categories.Replace("[", "").Replace("]","").Replace("\"","").Split(",");
			}
			else
			{
				return new string[] { Constants.ErrorNoCategoriesFound };
			}
		}
    }
}
