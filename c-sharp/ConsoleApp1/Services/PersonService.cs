using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
		public async Task<dynamic> GetNamesAsync()
        {
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                result = await client.GetStringAsync("");
            }
            return JsonConvert.DeserializeObject<dynamic>(result);
        }
    }
}
