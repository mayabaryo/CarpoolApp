using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CarpoolApp.Services;
using CarpoolApp.Helpers;
using CarpoolApp.Model;
using CarpoolApp.Models;
using System.Collections.ObjectModel;
using CarpoolApp.Views;

using Xamarin.Essentials;
using System.Linq;
using System.Text.RegularExpressions;
using CarpoolApp.DTO;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

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

        #region Origin
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
        #endregion

        #region Destination
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
        #endregion

        #region Waypoints
        private List<string> waypoints;
        public List<string> Waypoints
        {
            get => this.waypoints;
            set
            {
                this.waypoints = value;
                OnPropertyChanged("Waypoints");
            }
        }
        #endregion

        #region Color
        private string color;
        public string Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }
        #endregion

        #region Carpool
        private Carpool carpool;
        public Carpool Carpool
        {
            get => carpool;
            set
            {
                carpool = value;
                OnPropertyChanged("Carpool");
            }
        }
        #endregion

        #region InDrive
        //boolean indicates if the driver start the drive to destination
        private bool inDrive;
        public bool InDrive
        {
            get => inDrive;
            set
            {
                inDrive = value;
                OnPropertyChanged("InDrive");
            }
        }
        #endregion

        #region IsDriver
        private bool isDriver;
        public bool IsDriver
        {
            get => isDriver;
            set
            {
                isDriver = value;
                OnPropertyChanged("IsDriver");
            }
        }
        #endregion

        public ObservableCollection<Kid> KidList { get; }

        private CarpoolService service;

        #region Constructor
        public ShowMapViewModel(string origin, string dest, List<string> waypoints, List<Kid> kids, Carpool carpool)
        {
            this.Color = "Red";

            this.Origin = origin;
            this.Destination = dest;
            this.Waypoints = waypoints;
            this.Carpool = carpool;

            KidList = new ObservableCollection<Kid>();
            foreach (Kid k in kids)
            {
                this.KidList.Add(k);
            }
            InDrive = false;
            this.service = new CarpoolService();

            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            if (currentUser.Id == carpool.AdultId)
                IsDriver = true;
            else
            {
                ConnectToHubServer();
                IsDriver = false;
            }

            
            OnStart();
        }
        #endregion     
        private async void ConnectToHubServer()
        {
            await service.Connect(carpool.Id);
            service.RegisterToArrive(OnArriveToDestination);
            service.RegisterToKidOnBoard(OnKidOnBoard);
            service.RegisterToStartDrive(OnStartDrive);
            service.RegisterToLocation(OnDriverLocationUpdate);
        }
        public event Action<double, double> UpdateDriverLocationEvent;
        private void OnDriverLocationUpdate(double latitude, double longitude)
        {
            if (UpdateDriverLocationEvent != null)
            {
                Device.BeginInvokeOnMainThread(() => UpdateDriverLocationEvent(latitude, longitude));
                
            }
                
        }
        private void OnStartDrive()
        {

        }
        private async void OnArriveToDestination()
        {
            await App.Current.MainPage.DisplayAlert("נסיעה הושלמה", "הנהג הגיע ליעד והנסיעה הסתיימה", "בסדר");
            await App.Current.MainPage.Navigation.PopAsync();
            await this.service.Disconnect(carpool.Id);


        }
        private void OnKidOnBoard(int kidId)
        {
            Kid kid = KidList.Where(k => k.Id == kidId).FirstOrDefault();
            if (kid != null)
            {
                kid.IsInCarpool = true;
            }
        }

        private bool OnTimer()
        {
            GetLocation();
            return InDrive;
        }

        private async void GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();

                await this.service.SendLocation(Carpool.Id, location.Latitude, location.Longitude);
                
            }
            catch (Exception e)
            {
                // Unable to get location
                Console.WriteLine(e.Message);
            }
        }
        public GooglePlace RouteOrigin { get; private set; }
        public GooglePlace RouteDestination { get; private set; }
        public List<GooglePlace> RouteWaypoints { get; private set; }
        public GoogleDirection RouteDirections { get; private set; }

        #region Go
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
        #endregion

        #region Start
        public ICommand Start => new Command(OnStart);
        public async void OnStart()
        {
            try
            {
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                bool succeed = await proxy.CarpoolInProcessAsync(this.Carpool.Id);


                GoogleMapsApiService service = new GoogleMapsApiService();
                //find auto complete places first for origin and destination
                GooglePlaceAutoCompleteResult originPlaces = await service.GetPlaces(Origin);
                GooglePlaceAutoCompleteResult destPlaces = await service.GetPlaces(Destination);
                //extract the exact first google place for origin and destination
                //note that here i am taking the first suggestion but it will be better if you will
                //ask the user to choose which suggestion is better for him
                GooglePlace place1 = await service.GetPlaceDetails(originPlaces.AutoCompletePlaces[0].PlaceId);
                GooglePlace place2 = await service.GetPlaceDetails(destPlaces.AutoCompletePlaces[0].PlaceId);

                List<GooglePlace> places = new List<GooglePlace>();
                foreach (string point in Waypoints)
                {
                    GooglePlaceAutoCompleteResult pointPlaces = await service.GetPlaces(point);
                    GooglePlace pointplace = await service.GetPlaceDetails(pointPlaces.AutoCompletePlaces[0].PlaceId);
                    places.Add(pointplace);
                }

                RouteWaypoints = places;

                //get directions to move from origin to destination
                GoogleDirection direction = await service.GetDirectionsMulti($"{place1.Latitude}", $"{place1.Longitude}", $"{place2.Latitude}", $"{place2.Longitude}", RouteWaypoints);


                //update the properties so the main page class will have access to the information and fire the event to update the map
                RouteOrigin = place1;
                RouteDestination = place2;
                RouteDirections = direction;


                if (OnUpdateMapEvent != null)
                    OnUpdateMapEvent();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region NavigateCommand
        public ICommand NavigateCommand => new Command(OnNavigate);
        public async void OnNavigate()
        {
            try
            {
                await this.service.Connect(Carpool.Id);
                InDrive = true;
                await this.service.StartDrive(Carpool.Id);
                Device.StartTimer(TimeSpan.FromSeconds(10), () => OnTimer());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region EndCommand
        public ICommand EndCommand => new Command(OnEnd);
        public async void OnEnd()
        {
            try
            {
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                bool succeed = await proxy.CarpoolEndedAsync(this.Carpool.Id);

                App theApp = (App)App.Current;

                
                await this.service.SendArriveToDestination(Carpool.Id);
                InDrive = false;
                await this.service.Disconnect(Carpool.Id);
                await theApp.MainPage.Navigation.PopAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region KidInCommand
        public ICommand KidInCommand => new Command<Kid>(OnKidIn);
        public async void OnKidIn(Kid kid)
       {
            if (IsDriver)
            {
                if (!kid.IsInCarpool)
                    kid.IsInCarpool = true;
                else
                    kid.IsInCarpool = false;

                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                List<KidsOfAdult> kidsOfAdult = kid.KidsOfAdults.ToList();

                string body = $"{kid.IdNavigation.FirstName} {kid.IdNavigation.LastName}" + " כעת בהסעה לפעילות. ניתן לצפות במסלול בזמן אמת באפליקציה";
                foreach (KidsOfAdult kidsOf in kidsOfAdult)
                {
                    Adult adult = kidsOf.Adult;
                    string to = adult.IdNavigation.Email;
                    string toName = adult.IdNavigation.UserName;
                    bool isSent = await proxy.SendEmailAsync(body, to, toName);
                }
                await this.service.SendKidOnBoard(Carpool.Id, kid.Id);
            }            
        }
        #endregion

        #region KidsInCarpool
        public List<Kid> KidsInCarpool()
        {
            List<Kid> kidsInCarpool = new List<Kid>();

            foreach(Kid kid in this.KidList)
            {
                if (kid.IsInCarpool)
                    kidsInCarpool.Add(kid);
            }
            return kidsInCarpool;
        }
        #endregion

        public event Action OnUpdateMapEvent;
    }
}
