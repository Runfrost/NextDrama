using Microsoft.Maui.Controls;
using System;

namespace NextDrama.Views
{
    public partial class DramaDetail : ContentPage
    {
        public DramaDetail()
        {
            InitializeComponent();
        }

        private async void OnHomeTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // G� tillbaka till f�reg�ende sida
        }

        private void OnExploreTapped(object sender, EventArgs e)
        {
            // H�r kan du l�gga till logik f�r att visa dropdown eller navigera
            Console.WriteLine("?? OnExploreTapped triggered in DramaDetail");
            ExploreDropdownLayout.IsVisible = !ExploreDropdownLayout.IsVisible;
            CommunityDropdownLayout.IsVisible = false;
        }

        private void OnCommunityTapped(object sender, EventArgs e)
        {
            Console.WriteLine("?? OnCommunityTapped triggered in DramaDetail");
            CommunityDropdownLayout.IsVisible = !CommunityDropdownLayout.IsVisible;
            ExploreDropdownLayout.IsVisible = false;
        }

        private void OnCalendarTapped(object sender, EventArgs e)
        {
            Console.WriteLine("?? OnCalendarTapped triggered in DramaDetail");
        }

        private void OnPopupButtonClicked(object sender, EventArgs e)
        {
            PopupMenu.IsVisible = !PopupMenu.IsVisible; // V�xlar mellan synlig/osynlig
        }
    }
}

