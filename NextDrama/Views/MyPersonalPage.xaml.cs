using Microsoft.Maui.Controls;
using System;

namespace NextDrama.Views
{
    public partial class MyPersonalPage : ContentPage
    {
        public MyPersonalPage()
        {
            InitializeComponent();
        }

        private async void OnBackToHomeTapped(object sender, EventArgs e)
        {
            Console.WriteLine("?? Home-knappen trycktes i MyPersonalPage!");

            if (Navigation.NavigationStack.Count > 1)
            {
                Console.WriteLine($"?? NavigationStack längd: {Navigation.NavigationStack.Count}");
                await Navigation.PopAsync();
            }
            else
            {
                Console.WriteLine("?? Ingen sida att poppa, återgår till MainPage.");
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }
    }
}

