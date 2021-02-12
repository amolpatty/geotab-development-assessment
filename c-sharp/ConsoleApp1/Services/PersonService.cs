using JokeGenerator.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    class PersonService: IPersonService
    {
        string _url = "";

        // Recommended pattern as per this article
        // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        // however this makes it difficult to unit test
        private static readonly HttpClient client = new HttpClient();

        public PersonService(string endpoint)
        {
            _url = endpoint;
            client.BaseAddress = new Uri(_url);
        }
        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public async Task<IPerson> GetNamesAsync()
        {
            string jsonResult = string.Empty;
            // switch to static httpclient see reference article
            /*
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                result = await client.GetStringAsync("");
            }
            */
            
            jsonResult = await client.GetStringAsync("");

            // use strongly typed person object instead of dynamic for efficiency
            return JsonConvert.DeserializeObject<Person>(jsonResult, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });

            /*
            // dynamic alternative to strongly typed person
            dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
            return new Person(result?.name?.ToString(), result?.surname?.ToString(), result?.gender?.ToString());
            */
        }
    }
}
