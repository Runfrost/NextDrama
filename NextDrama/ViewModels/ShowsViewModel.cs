using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using NextDrama.Models;
using NextDrama.Services;

namespace NextDrama.ViewModels
{
    public class ShowsViewModel
    {

        //hämtar TV-serier från API:et och lagrar dem i  ObservableCollection<TvShow> som sedan visas i UI:t
        public ObservableCollection<TvShow> Shows { get; set; } = new();

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
    }
}


