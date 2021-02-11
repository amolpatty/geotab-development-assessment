using System;
using System.Net.Http;
using System.Threading.Tasks;
using JokeGenerator;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";
		static int _numberOfJokes = 1;

        public JsonFeed() { }
        public JsonFeed(string endpoint, int numberOfJokes)
        {
            _url = endpoint;
			_numberOfJokes = numberOfJokes;
        }
        
		public static string[] GetRandomJokes(IPerson person, string category)
		{
			// todo: use using
			// validate category against actual categories

			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "jokes/random";
			if (category != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "category=";
				url += category;
			}

            string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

            if (person?.FirstName != null && person?.LastName != null)
            {
				// replace with String.Replace
				/*
                int index = joke.IndexOf(Constants.ChuckNorris);
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + Constants.ChuckNorris.Length, joke.Length - (index + Constants.ChuckNorris.Length));
                joke = firstPart + " " + firstname + " " + lastname + secondPart;
				*/

				joke = joke.Replace(Constants.ChuckNorris, person?.FirstName + " " + person?.LastName);
            }

            return new string[] { JsonConvert.DeserializeObject<dynamic>(joke).value };
        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic GetNames()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			var result = client.GetStringAsync("").Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
		}

		public static string[] GetCategories()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			// fixed the url here
			return new string[] { Task.FromResult(client.GetStringAsync("jokes/categories").Result).Result };
		}
    }
}
