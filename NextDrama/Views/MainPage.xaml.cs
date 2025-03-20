using NextDrama.ViewModels;
using System.Timers;
using Microsoft.Maui.Dispatching;

namespace NextDrama
{
    public partial class MainPage : ContentPage
    {
        private readonly string[] images = { "sk.png", "header2.png", "header3.png" };
        private int currentImageIndex = 0;
        private System.Timers.Timer imageTimer;

        public MainPage()
        {
            InitializeComponent(); // 🔹 Se till att XAML laddas korrekt
            BindingContext = new MainViewModel();
            StartImageRotation();
        }

        private void StartImageRotation()
        {
            imageTimer = new System.Timers.Timer(5000);
            imageTimer.Elapsed += OnTimerElapsed;
            imageTimer.AutoReset = true;
            imageTimer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (HeaderImage != null)
                {
                    await HeaderImage.FadeTo(0, 500);
                    currentImageIndex = (currentImageIndex + 1) % images.Length;
                    HeaderImage.Source = images[currentImageIndex];
                    await HeaderImage.FadeTo(1, 500);
                }
            });
        }

        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }
    }
}




