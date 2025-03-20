using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using NextDrama.ViewModels;

namespace NextDrama.Views
{
    public partial class ShowsPage : ContentPage
    {
        private readonly ShowsViewModel _viewModel; // Skapar en instans av ViewModel

        public ShowsPage()
        {
            Console.WriteLine("✅ ShowsPage KONSTRUKTORN KÖRS");
            InitializeComponent();
            BindingContext = _viewModel = new ShowsViewModel(); // Kopplar UI till ViewModel
            _viewModel.FetchTvShowsAsync(); // Hämtar TV-serier vid sidladdning
        }

        private async void OnHomeTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnExploreTapped(object sender, EventArgs e)
        {
            ExploreDropdownLayout.IsVisible = !ExploreDropdownLayout.IsVisible;
            CommunityDropdownLayout.IsVisible = false;
        }

        private void OnCommunityTapped(object sender, EventArgs e)
        {
            CommunityDropdownLayout.IsVisible = !CommunityDropdownLayout.IsVisible;
            ExploreDropdownLayout.IsVisible = false;
        }
        private void OnSearchCompleted(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ShowsViewModel;
            if (viewModel?.SearchCommand.CanExecute(null) == true)
            {
                viewModel.SearchCommand.Execute(null);
            }
        }
    }
}


