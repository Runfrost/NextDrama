using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextDrama.ViewModels
{
    public class ShowsViewModel
    {
        public ObservableCollection<Show> Shows { get; set; }

        public ShowsViewModel()
        {
            Shows = new ObservableCollection<Show>
            {
                new Show { Title = "Breaking Bad", ImageUrl="breakingbad.jpg" },
                new Show { Title = "Game of Thrones", ImageUrl="got.jpg" },
                new Show { Title = "Stranger Things", ImageUrl="strangerthings.jpg" }
            };
        }
    }

    public class Show
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
