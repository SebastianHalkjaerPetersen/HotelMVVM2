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
    class GuestHandler
    {
        public GuestViewModel GuestViewModel { get; set; }

        private PersistencyFacade guestFacade;

        private const string guestURI = "Guests";




        public GuestHandler(GuestViewModel guestViewModel)
        {
            GuestViewModel = guestViewModel;
            guestFacade = new PersistencyFacade();
        }

        private void RefreshList()
        {
            GuestViewModel.GuestCatalogSingleton.LoadGuests();
        }

        public async void CreateGuest()
        {
            await guestFacade.Post<Guest>(GuestViewModel.NewGuest, guestURI);
            RefreshList();
        }

        public async void UpdateGuest()
        {
            await guestFacade.Put(GuestViewModel.UpdateID, GuestViewModel.NewGuest, guestURI);
            RefreshList();
        }

        public async void DeleteGuest()
        {
            await guestFacade.DeleteOne(GuestViewModel.NewGuest.ID, guestURI);
            RefreshList();
        }

        public async void Clear()
        {
            await GuestViewModel.ClearNewGuest();
        }

        //public async void DeleteAllGuests()
        //{
        //    await guestFacade.DeleteAll<Guest>(guestURI);
        //    RefreshList();
        //}
    }
}
