using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NextDrama.Services
{
    public class ApiService
    {
        private static readonly Lazy<ApiService> _instance = new(() => new ApiService());
        public static ApiService Instance => _instance.Value;

        private static readonly HttpClient client = new HttpClient();
        private const string ApiKey = "adf5e21c98bbdec41ee0a487cc7ea399";
        private const string BaseUrl = "https://api.themoviedb.org/3/discover/tv?";

        private ApiService() { } // Privat konstruktor för Singleton

        public async Task<string> GetRawApiResponseAsync(string parameters)
        {
            try
            {
                string fullUrl = $"{BaseUrl}api_key={ApiKey}{parameters}";
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API Error: {response.StatusCode}");

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ API Error: {ex.Message}");
                return null;
            }
        }
    }
}



