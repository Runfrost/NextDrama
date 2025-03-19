using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using NextDrama.Models;
using NextDrama.Services;
using Microsoft.Maui.Controls;

namespace NextDrama.ViewModels
{
    public class ShowsViewModel
    {
        public ObservableCollection<TvShow> Shows { get; set; } = new();
        public string SearchQuery { get; set; } // 🔹 Lagrar vad användaren skriver i sökrutan
        public ICommand SearchCommand { get; }

        public ShowsViewModel()
        {
            SearchCommand = new Command(async () => await SearchTvShowsAsync());
            _ = FetchTvShowsAsync(); // 🔹 Hämta koreanska serier när sidan laddas
        }

        public async Task FetchTvShowsAsync()
        {
            string apiUrl = "&language=en-US&sort_by=popularity.desc&page=1&with_original_language=ko";
            string jsonResponse = await ApiService.Instance.GetRawApiResponseAsync(apiUrl);

            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var tvShows = JsonSerializer.Deserialize<TvShowResponse>(jsonResponse);
                if (tvShows?.Results != null)
                {
                    Shows.Clear();
                    foreach (var show in tvShows.Results)
                    {
                        Shows.Add(show);
                    }
                }
            }
        }

        public async Task SearchTvShowsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                await FetchTvShowsAsync(); // 🔹 Om fältet är tomt, visa alla koreanska serier igen
                return;
            }

            string jsonResponse = await ApiService.Instance.SearchTvShowsAsync(SearchQuery);
            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var searchResults = JsonSerializer.Deserialize<TvShowResponse>(jsonResponse);
                if (searchResults?.Results != null)
                {
                    Shows.Clear(); // 🔹 Töm listan innan vi fyller på med sökresultaten
                    foreach (var show in searchResults.Results)
                    {
                        Shows.Add(show);
                    }
                }
            }
        }
    }
}



