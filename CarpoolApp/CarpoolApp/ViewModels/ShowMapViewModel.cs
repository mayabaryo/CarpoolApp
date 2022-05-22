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

            this.service = new CarpoolService();
            this.service.RegisterToArrive(OnArriveToDestination);

            


            OnStart();
        }
        #endregion     
        void OnArriveToDestination()
        {

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

                //List<Pin> pins = new List<Pin>();
                //for (int i = 0; i < RouteWaypoints.Count; i++)
                //{

                //}  
                //foreach (GooglePlace route in RouteWaypoints)
                //{
                //    Pin pin = new Pin
                //    {
                //        Type = PinType.Place,
                //        Position = new Position(route.Latitude, route.Longitude),
                //        Label = "נקודת איסוף",
                //        Address = route.Name
                //    };
                //    pins.Add(pin);
                //}


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
        public void OnNavigate()
        {
            try
            {


                //CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                //bool succeed = await proxy.CarpoolEndedAsync(this.Carpool.Id);

                //App theApp = (App)App.Current;

                //Page page = new AdultMainTab();
                //page.Title = "שלום " + theApp.CurrentUser.UserName;
                //theApp.MainPage = new NavigationPage(page) /*{ BarBackgroundColor = Color.FromHex("#81cfe0") }*/;

                //await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);

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

                Page page = new AdultMainTab();
                page.Title = "שלום " + theApp.CurrentUser.UserName;
                theApp.MainPage = new NavigationPage(page) /*{ BarBackgroundColor = Color.FromHex("#81cfe0") }*/;

                //await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region KidInCommand
        public ICommand KidInCommand => new Command<Kid>(OnKidIn);
        public /*async*/ void OnKidIn(Kid kid)
       {
            //this.Color = "LightGreen";
            kid.IsInCarpool = true;

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<KidsOfAdult> kidsOfAdult = kid.KidsOfAdults.ToList();

            string body = $"{kid.IdNavigation.FirstName} {kid.IdNavigation.LastName}" + " כעת בהסעה לפעילות. ניתן לצפות במסלול בזמן אמת באפליקציה";
            //string body = "בקשתך לצרף את " + kid.IdNavigation.UserName + " להסעה אושרה על ידי " + currentUser.UserName;
            //foreach (KidsOfAdult kidsOf in kidsOfAdult)
            //{
            //    Adult adult = kidsOf.Adult;
            //    string to = adult.IdNavigation.Email;
            //    string toName = adult.IdNavigation.UserName;
            //    bool isSent = await proxy.SendEmailAsync(body, to, toName);
            //}
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
