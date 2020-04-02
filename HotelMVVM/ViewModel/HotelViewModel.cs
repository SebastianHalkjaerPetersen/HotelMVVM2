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
    public class HotelViewModel : INotifyPropertyChanged
    {
        public HotelCatalogSingleton HotelCatalogSingleton { get; set; }

        public ICommand CreateHotelCommand { get; set; }
        
        public ICommand UpdateHotelCommand { get; set; }

        public ICommand DeleteHotelCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        private Hotel _newHotel;

        public Hotel NewHotel
        {
            get { return _newHotel; }
            set { _newHotel = value; OnPropertyChanged(); }
        }

        public HotelHandler HotelHandler { get; set; }

        private int _updateID;

        public int UpdateID
        {
            get { return _updateID; }
            set { _updateID = value; OnPropertyChanged();}
        }

        public async Task<bool> ClearNewHotel()
        {
            NewHotel = null;
            return true;
        }

        public HotelViewModel()
        {
            HotelHandler = new HotelHandler(this);
            HotelCatalogSingleton = HotelCatalogSingleton.Instance;
            NewHotel = new Hotel();
            CreateHotelCommand = new RelayCommand(HotelHandler.CreateHotel, () => NewHotel!=null);
            UpdateHotelCommand = new RelayCommand(HotelHandler.UpdateHotel, () => NewHotel!=null);
            DeleteHotelCommand = new RelayCommand(HotelHandler.DeleteHotel,() => NewHotel != null);
            ClearCommand = new RelayCommand(HotelHandler.Clear, () => NewHotel != null);
            
        }

        












        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
