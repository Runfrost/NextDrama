using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using NextDrama.Models;
using NextDrama.Views;
using System.Diagnostics;

namespace NextDrama.ViewModels
{

    public class MainViewModel
    {
        public ObservableCollection<string> Categories { get; set; }
        public ICommand CategorySelectedCommand { get; }

        public ObservableCollection<Drama> RecommendedDramas { get; set; }
        public ObservableCollection<Drama> ComingIn2025 { get; set; }
        public ObservableCollection<Drama> TopVarietyShows { get; set; }
        public ObservableCollection<Drama> BottomImages { get; set; }

        public ICommand ImageTappedCommand { get; }

        public MainViewModel()
        {
            // 🔹 RECOMMENDED DRAMAS - Endast första bilden skickar till DramaDetail.xaml
            RecommendedDramas = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="aa.png" }, // 🔹 Endast denna leder till DramaDetail.xaml
                new Drama { ImageUrl="b2.png" }, // 🔹 Dessa bilder kan tryckas men gör inget
                new Drama { ImageUrl="b3.png" },
                new Drama { ImageUrl="gg.png" },
                new Drama { ImageUrl="hh.png" },
                new Drama { ImageUrl="ii.png" }
            };

            // 🔹 COMING IN 2025 - Samma logik som RecommendedDramas
            ComingIn2025 = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="b1.png" },
                new Drama { ImageUrl="bb.png" },
                new Drama { ImageUrl="cc.png" },
                new Drama { ImageUrl="dd.png" },
                new Drama { ImageUrl="ee.png" },
                new Drama { ImageUrl="ff.png" }
            };

            // 🔹 TOP VARIETY SHOWS - Samma logik som RecommendedDramas
            TopVarietyShows = new ObservableCollection<Drama>
            {
                new Drama { ImageUrl="a1.png" },
                new Drama { ImageUrl="a2.png" },
                new Drama { ImageUrl="a3.png" },
                new Drama { ImageUrl="a4.png" },
                new Drama { ImageUrl="a5.png" },
                new Drama { ImageUrl="a6.png" }
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
            // 🔹 GÖR BILDERNA KLICKBARA
            ImageTappedCommand = new Command<Drama>(async (selectedDrama) =>
            {
                if (selectedDrama != null)
                {
                    // 🔹 Om det är den FÖRSTA bilden i RecommendedDramas, öppna DramaDetail.xaml
                    if (RecommendedDramas.IndexOf(selectedDrama) == 0)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new DramaDetail());
                    }
                    else
                    {
                        // 🔹 Alla andra bilder i alla listor gör ingenting
                        await Application.Current.MainPage.DisplayAlert("Info", "Den här bilden gör ingenting!", "OK");
                    }
                }
            });
        }
    }
}

