using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using NextDrama.Models;
using NextDrama.Views;

namespace NextDrama.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Drama> RecommendedDramas { get; set; }
        public ObservableCollection<Drama> ComingIn2025 { get; set; }
        public ObservableCollection<Drama> TopVarietyShows { get; set; }
        public ObservableCollection<Drama> BottomImages { get; set; }

        public ICommand CategorySelectedCommand { get; }
        public ICommand ImageTappedCommand { get; }

        public MainViewModel()
        {
            RecommendedDramas = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="aa.png" },
                new Drama { ImageUrl="b2.png" },
                new Drama { ImageUrl="b3.png" },
                new Drama { ImageUrl="gg.png" },
                new Drama { ImageUrl="hh.png" },
                new Drama { ImageUrl="ii.png" }
            };

            ComingIn2025 = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="b1.png" },
                new Drama { ImageUrl="bb.png" },
                new Drama { ImageUrl="cc.png" },
                new Drama { ImageUrl="dd.png" },
                new Drama { ImageUrl="ee.png" },
                new Drama { ImageUrl="ff.png" }
            };

            TopVarietyShows = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="a1.png" },
                new Drama { ImageUrl="a2.png" },
                new Drama { ImageUrl="a3.png" },
                new Drama { ImageUrl="a4.png" },
                new Drama { ImageUrl="a5.png" },
                new Drama { ImageUrl="a6.png" }
            };

            BottomImages = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="ex.png" },
                new Drama { ImageUrl="ml.png" },
                new Drama { ImageUrl="fe.png" },
                new Drama { ImageUrl="pr.png" }
            };

            CategorySelectedCommand = new Command<string>(async (selectedCategory) =>
            {
                switch (selectedCategory)
                {
                    case "SHOWS":
                        await Application.Current.MainPage.Navigation.PushAsync(new ShowsPage());
                        break;
                    case "MOVIES":
                        await Application.Current.MainPage.DisplayAlert("MOVIES", "Här kommer filmsidan snart!", "OK");
                        break;
                    case "NEWS":
                        await Application.Current.MainPage.DisplayAlert("NEWS", "Här kommer nyheter snart!", "OK");
                        break;
                    case "FOR YOU":
                        await Application.Current.MainPage.DisplayAlert("FOR YOU", "Här kommer rekommenderade serier!", "OK");
                        break;
                }
            });

            ImageTappedCommand = new Command<object>(async (param) =>
            {
                if (param is Drama selectedDrama)
                {
                    // Om det är en Drama-bild som klickas
                    if (RecommendedDramas.Contains(selectedDrama) && RecommendedDramas.IndexOf(selectedDrama) == 0)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new DramaDetail());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Info", "Den här bilden gör ingenting!", "OK");
                    }
                }
                else if (param is string strParam && strParam == "pr")
                {
                    // Om det är knappen med `pr.png` som klickas
                    await Application.Current.MainPage.Navigation.PushAsync(new MyPersonalPage());
                }
            });

        }
    }
}


