using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMVVM.Persistency;
using ModelLibrary;

namespace HotelMVVM.Model
{
    class GuestCatalogSingleton
    {
        private static GuestCatalogSingleton _instance = null;

        public static GuestCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GuestCatalogSingleton();
                }
                return _instance;
            }
        }

        public ObservableCollection<Guest> Guests { get; set; }


        private GuestCatalogSingleton()
        {
            Guests = new ObservableCollection<Guest>();
            //Hotel h1 = new Hotel(1,"asd", "strandvejen 213");
            //Hotel h2 = new Hotel(2, "qwe", "Københavnsvej 444");
            //Hotel h3 = new Hotel(3, "zxc", "Lærkevangen 2");
            //Hotels.Add(h1);
            //Hotels.Add(h2);
            //Hotels.Add(h3);

            LoadGuests();
        }

        public async void LoadGuests()
        {
            Guests.Clear();
            PersistencyFacade facade = new PersistencyFacade();
            List<Guest> guestList = await facade.GetAll<Guest>("Guests");
            foreach (Guest guest in guestList)
            {
                Guests.Add(guest);
            }
        }
    }
}
