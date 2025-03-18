using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NextDrama.Services
{
    public class ApiService
    {
        // Din API-nyckel (lägg till den här)
        private static readonly string apiKey = "adf5e21c98bbdec41ee0a487cc7ea399"; // Ersätt med din verkliga API-nyckel
        private static readonly string baseUrl = "https://api.themoviedb.org/3/discover/tv?"; // Bas-URL för att bygga URL:en
        private static readonly HttpClient client = new HttpClient();

        // Metod som tar emot URL som parameter och returnerar API-svaret
        public async Task<string> GetRawApiResponse(string url)
        {
            try
            {
                // Bygg hela URL:en med din API-nyckel
                string fullUrl = $"{baseUrl}api_key={apiKey}{url}";

                // Skicka GET-anrop till den kompletta URL:en
                HttpResponseMessage response = await client.GetAsync(fullUrl);

                // Kolla om anropet lyckades
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"API Error: {response.StatusCode}");

                // Läs innehållet i svaret och returnera det som en sträng
                string json = await response.Content.ReadAsStringAsync();
                return json; // Returnerar rå JSON som vi kan testa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching TV shows: {ex.Message}");
                return null; // Om något går fel, returnera null
            }
        }
    }
}



