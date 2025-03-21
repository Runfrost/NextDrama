﻿using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using NextDrama.Models;
using NextDrama.Services;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using static NextDrama.Services.ApiService;

namespace NextDrama.ViewModels
{
    public class ShowsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TvShow> Shows { get; set; } = new();

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    _ = SearchTvShowsAsync(); // Live sökningen aktiveras här
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddToUserListCommand { get; }

        public ShowsViewModel()
        {
            SearchCommand = new Command(async () => await SearchTvShowsAsync());
            AddToUserListCommand = new Command<TvShow>(async (selectedShow) => await ShowCategoryPopup(selectedShow));
            _ = FetchTvShowsAsync();
        }

        public async Task FetchTvShowsAsync()
        {
            string apiUrl = "&language=en-US&sort_by=popularity.desc&page=1&with_original_language=ko";
            string jsonResponse = await ApiService.Instance.GetRawApiResponseAsync(apiUrl); //Här anropas ApiService.Instance

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
                await FetchTvShowsAsync(); // Om fältet är tomt, visa alla koreanska serier igen
                return;
            }

            string jsonResponse = await ApiService.Instance.SearchTvShowsAsync(SearchQuery);//Skickar en förfrågan till TMDb:s API
            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var searchResults = JsonSerializer.Deserialize<TvShowResponse>(jsonResponse);
                if (searchResults?.Results != null)
                {
                    Shows.Clear();
                    foreach (var show in searchResults.Results)
                    {
                        Shows.Add(show);
                    }
                }
            }
        }



        private async Task ShowCategoryPopup(TvShow show)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet(
                "Välj kategori", "Avbryt", null, "Ser på", "Vill se", "Har sett");

            if (action != "Avbryt" && action != null)
            {
                UserListService.AddToUserList(show, action);
                await Application.Current.MainPage.DisplayAlert("✅ Tillagd!", $"{show.Name} har lagts till i \"{action}\"", "OK");
            }
        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}




