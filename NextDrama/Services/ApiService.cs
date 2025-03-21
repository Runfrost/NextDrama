using System;
using NextDrama.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage; 

namespace NextDrama.Services
{
    public class ApiService
    {
        // API`t är ifrån TMDB
        // Singelton
        


        private static readonly Lazy<ApiService> _instance = new(() => new ApiService());
        public static ApiService Instance => _instance.Value;

        private static readonly HttpClient client = new HttpClient();
        private const string ApiKey = "adf5e21c98bbdec41ee0a487cc7ea399";
        private const string BaseUrl = "https://api.themoviedb.org/3/discover/tv?";



        private ApiService() { } // Privat konstruktor

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



        public async Task<string> SearchTvShowsAsync(string query)
        {
            try
            {
                string apiUrl = $"https://api.themoviedb.org/3/search/tv?api_key={ApiKey}&language=en-US&query={Uri.EscapeDataString(query)}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API Error: {response.StatusCode}");

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Search API Error: {ex.Message}");
                return null;
            }
        }




}
}



