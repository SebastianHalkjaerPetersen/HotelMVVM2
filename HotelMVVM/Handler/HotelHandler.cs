using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMVVM.Persistency;
using HotelMVVM.ViewModel;
using ModelLibrary;

namespace HotelMVVM.Handler
{
    public class HotelHandler
    {
        public HotelViewModel HotelViewModel { get; set; }

        private PersistencyFacade hotelFacade;

        private const string hotelURI = "Hotels";

        


        public HotelHandler(HotelViewModel hotelViewModel)
        {
            HotelViewModel = hotelViewModel;
            hotelFacade = new PersistencyFacade();
        }

        private void RefreshList()
        {
            HotelViewModel.HotelCatalogSingleton.LoadHotels();
        }

        public async void CreateHotel()
        {
            await hotelFacade.Post<Hotel>(HotelViewModel.NewHotel, hotelURI);
            RefreshList();
        }

        public async void UpdateHotel()
        {
            await hotelFacade.Put(HotelViewModel.UpdateID, HotelViewModel.NewHotel, hotelURI);
            RefreshList();
        }

        public async void DeleteHotel()
        {
            await hotelFacade.DeleteOne(HotelViewModel.NewHotel.ID, hotelURI);
            RefreshList();
        }

        public async void DeleteHotel(Hotel hotel)
        {
            await hotelFacade.DeleteOne(hotel.ID, hotelURI);
            RefreshList();
        }

        public async void Clear()
        {
            await HotelViewModel.ClearNewHotel();
        }

        //public void DeleteAll()
        //{
        //    foreach (Hotel hotel in HotelViewModel.HotelCatalogSingleton.Hotels)
        //    {
        //        DeleteHotel(hotel);
        //    }
        //}
    }
}
