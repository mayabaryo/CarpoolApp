using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CarpoolApp.Services;
using CarpoolApp.Helpers;
using CarpoolApp.Model;

namespace CarpoolApp.ViewModels
{
    class ShowMapViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string origin;
        public string Origin
        {
            get => this.origin;
            set
            {
                this.origin = value;
                OnPropertyChanged("Origin");
            }
        }

        private string destination;
        public string Destination
        {
            get => this.destination;
            set
            {
                this.destination = value;
                OnPropertyChanged("Destination");
            }
        }

        public GooglePlace RouteOrigin { get; private set; }
        public GooglePlace RouteDestination { get; private set; }
        public GoogleDirection RouteDirections { get; private set; }

        public ICommand Go => new Command(OnGo);
        public async void OnGo()
        {
            try
            {
                GoogleMapsApiService service = new GoogleMapsApiService();
                //find auto complete places first for origin and destination
                GooglePlaceAutoCompleteResult originPlaces = await service.GetPlaces(Origin);
                GooglePlaceAutoCompleteResult destPlaces = await service.GetPlaces(Destination);
                //extract the exact first google place for origin and destination
                //note that here i am taking the first suggestion but it will be better if you will
                //ask the user to choose which suggestion is better for him
                GooglePlace place1 = await service.GetPlaceDetails(originPlaces.AutoCompletePlaces[0].PlaceId);
                GooglePlace place2 = await service.GetPlaceDetails(destPlaces.AutoCompletePlaces[0].PlaceId);
                //get directions to move from origin to destination
                GoogleDirection direction = await service.GetDirections($"{place1.Latitude}", $"{place1.Longitude}", $"{place2.Latitude}", $"{place2.Longitude}");
                //update the properties so the main page class will have access to the information and fire the event to update the map
                RouteOrigin = place1;
                RouteDestination = place2;
                RouteDirections = direction;
                if (OnUpdateMapEvent != null)
                    OnUpdateMapEvent();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
        }

        public event Action OnUpdateMapEvent;
    }
}
