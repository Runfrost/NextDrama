using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using NextDrama.Services;
using NextDrama.Models;

namespace NextDrama.Views
{
    public partial class ShowsPage : ContentPage
    {
        private readonly ApiService _apiService = new ApiService(); // Använder API-tjänsten

        public ShowsPage()
        {
            Console.WriteLine("✅ ShowsPage KONSTRUKTORN KÖRS");
            InitializeComponent();  // Gör att komponenterna kopplas samman
            FetchTvShowsAsync(); // Hämta serier vid sidladdning
        }

        private async void OnCalendarTapped(object sender, EventArgs e)
        {
            // Ta bort den här metoden om du inte behöver använda den
        }
        private async void OnHomeTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Gå tillbaka till startsidan
        }

        

        // Metoden som triggas när användaren trycker på Explore-knappen
        private void OnExploreTapped(object sender, EventArgs e)
        {
            // Visa eller dölja ExploreDropdownLayout
            ExploreDropdownLayout.IsVisible = !ExploreDropdownLayout.IsVisible;
            // Se till att CommunityDropdownLayout inte visas
            CommunityDropdownLayout.IsVisible = false;
        }

        // Metoden som triggas när användaren trycker på Community-knappen
        private void OnCommunityTapped(object sender, EventArgs e)
        {
            // Visa eller dölja CommunityDropdownLayout
            CommunityDropdownLayout.IsVisible = !CommunityDropdownLayout.IsVisible;
            // Se till att ExploreDropdownLayout inte visas
            ExploreDropdownLayout.IsVisible = false;
        }

        private async void FetchTvShowsAsync()
        {
            Console.WriteLine("✅ FetchTvShowsAsync is running");

            try
            {
                // API-url som hämtar koreanska serier
                string apiUrl = "&language=en-US&sort_by=popularity.desc&page=1&with_original_language=ko";

                // Hämta JSON-svaret från API via ApiService
                string jsonResponse = await _apiService.GetRawApiResponse(apiUrl);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    Console.WriteLine("✅ API Response:");
                    Console.WriteLine(jsonResponse); // Skriver ut hela JSON-svaret till konsolen

                    var tvShows = JsonSerializer.Deserialize<TvShowResponse>(jsonResponse);

                    if (tvShows != null && tvShows.Results != null)
                    {
                        Console.WriteLine($"🔹 Totalt antal koreanska serier: {tvShows.Results.Count}");

                        // Sätt den filtrerade listan som ItemsSource för CollectionView
                        ShowsCollectionView.ItemsSource = tvShows.Results;
                    }
                    else
                    {
                        Console.WriteLine("⚠️ API-svaret har inga serier");
                        await DisplayAlert("Error", "No Korean TV shows found.", "OK");
                    }
                }
                else
                {
                    Console.WriteLine("⚠️ API-anropet misslyckades");
                    await DisplayAlert("Error", "Failed to fetch TV shows.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ API Error: {ex.Message}");
                await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
            }
        }
    }
}
