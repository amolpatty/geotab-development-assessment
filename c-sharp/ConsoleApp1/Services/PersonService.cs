using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace JokeGenerator.Services
{
    class PersonService: IPersonService
    {
        string _url = "";

        public PersonService(string endpoint)
        {
            _url = endpoint;            
        }
        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public dynamic GetNames()
        {
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                result = client.GetStringAsync("").Result;
            }
            return JsonConvert.DeserializeObject<dynamic>(result);
        }
    }
}
