using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using HotelMVVM.Persistency;
using ModelLibrary;

namespace HotelMVVM.Model
{
    public class HotelCatalogSingleton
    {
        private static HotelCatalogSingleton _instance = null;

        public static HotelCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HotelCatalogSingleton();
                }
                return _instance;
            }
        }

        public ObservableCollection<Hotel> Hotels { get; set; }
        

        private HotelCatalogSingleton()
        {
            Hotels = new ObservableCollection<Hotel>();
            //Hotel h1 = new Hotel(1,"asd", "strandvejen 213");
            //Hotel h2 = new Hotel(2, "qwe", "Københavnsvej 444");
            //Hotel h3 = new Hotel(3, "zxc", "Lærkevangen 2");
            //Hotels.Add(h1);
            //Hotels.Add(h2);
            //Hotels.Add(h3);

            LoadHotels();
        }

        public async void LoadHotels()
        {
            Hotels.Clear();
            PersistencyFacade facade = new PersistencyFacade();
            List<Hotel> hotelList = await facade.GetAll<Hotel>("Hotels");
            foreach (Hotel hotel in hotelList)
            {
                    Hotels.Add(hotel);
            }
        }
    }
}
