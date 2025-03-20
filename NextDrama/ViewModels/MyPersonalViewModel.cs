using NextDrama.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NextDrama.Models;
using Microsoft.Maui.Controls;
using System.Linq; // 🔹 Behövs för LINQ-metoder

namespace NextDrama.ViewModels
{
    public class MyPersonalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TvShow> WatchingNow { get; set; } = new();
        public ObservableCollection<TvShow> WantToWatch { get; set; } = new();
        public ObservableCollection<TvShow> Watched { get; set; } = new();

        public MyPersonalViewModel()
        {
            LoadUserLists();

            // 🔹 Uppdatera sidan automatiskt när något ändras
            MessagingCenter.Subscribe<ShowsViewModel>(this, "UpdatePersonalPage", (sender) =>
            {
                LoadUserLists();
            });
        }

        private void LoadUserLists()
        {
            var userList = UserListService.GetUserList();

            WatchingNow.Clear();
            WantToWatch.Clear();
            Watched.Clear();

            foreach (var entry in userList.Values)
            {
                switch (entry.Category)
                {
                    case "Ser på":
                        WatchingNow.Add(entry.Show);
                        break;
                    case "Vill se":
                        WantToWatch.Add(entry.Show);
                        break;
                    case "Har sett":
                        Watched.Add(entry.Show);
                        break;
                }
            }

            OnPropertyChanged(nameof(WatchingNow));
            OnPropertyChanged(nameof(WantToWatch));
            OnPropertyChanged(nameof(Watched));
        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}





