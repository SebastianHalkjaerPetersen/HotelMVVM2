using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HotelMVVM.Annotations;
using HotelMVVM.Common;
using HotelMVVM.Handler;
using HotelMVVM.Model;
using ModelLibrary;

namespace HotelMVVM.ViewModel
{
    class GuestViewModel : INotifyPropertyChanged
    {
        public GuestCatalogSingleton GuestCatalogSingleton { get; set; }

        public ICommand CreateGuestCommand { get; set; }

        public ICommand UpdateGuestCommand { get; set; }

        public ICommand DeleteGuestCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        private Guest _newGuest;

        public Guest NewGuest
        {
            get { return _newGuest; }
            set
            {
                _newGuest = value;
                OnPropertyChanged();
            }
        }

        public GuestHandler GuestHandler { get; set; }

        private int _updateID;

        public int UpdateID
        {
            get { return _updateID; }
            set { _updateID = value; OnPropertyChanged(); }
        }

        public async Task<bool> ClearNewGuest()
        {
            NewGuest = null;
            return true;
        }

        public GuestViewModel()
        {
            GuestHandler = new GuestHandler(this);
            GuestCatalogSingleton = GuestCatalogSingleton.Instance;
            NewGuest = new Guest();
            CreateGuestCommand = new RelayCommand(GuestHandler.CreateGuest, () => NewGuest != null);
            UpdateGuestCommand = new RelayCommand(GuestHandler.UpdateGuest, () => NewGuest != null);
            DeleteGuestCommand = new RelayCommand(GuestHandler.DeleteGuest,() => NewGuest != null);
            ClearCommand = new RelayCommand(GuestHandler.Clear, () => NewGuest != null);
        }














        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
