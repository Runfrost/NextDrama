using Microsoft.Maui.Controls;

namespace NextDrama
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new AppShell())); // FIX: NavigationPage krävs för PushAsync()
        }
    }
}

