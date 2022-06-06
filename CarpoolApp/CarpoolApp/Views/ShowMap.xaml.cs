using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarpoolApp.ViewModels;
using CarpoolApp.Helpers;
using Xamarin.Forms.Maps;
using CarpoolApp.Model;
using CarpoolApp.Services;
using CarpoolApp.Models;

namespace CarpoolApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMap : ContentPage
    {
        private Circle driver;
        public ShowMap(string origin, string dest, List<string> waypoints, List<Kid> kids, Carpool carpool)
        {
            driver = null;
            ShowMapViewModel vm = new ShowMapViewModel(origin, dest, waypoints, kids, carpool);
            vm.OnUpdateMapEvent += OnUpdateMap;
            vm.UpdateDriverLocationEvent += OnDriverLocationUpdate;
            this.BindingContext = vm;
            InitializeComponent();
        }

        public void OnDriverLocationUpdate(double latitude, double longitude)
        {
            Position pos = new Position(latitude, longitude);
            if (driver == null)
            {
                this.driver = new Circle()
                {
                    StrokeColor = Color.White,
                    FillColor = Color.Orange,
                    Center = pos,
                    Radius = Distance.FromMeters(150)
                };
                map.MapElements.Add(this.driver);
            }
            else
                this.driver.Center = pos;
        }

        public void OnUpdateMap()
        {
            //List<GooglePlace> places = new List<GooglePlace>();
            //GoogleMapsApiService service = new GoogleMapsApiService();
            ////find auto complete places first for origin and destination
            //GooglePlaceAutoCompleteResult autoOrigin = await service.GetPlaces("הוד השרון");
            //GooglePlace origin = await service.GetPlaceDetails(autoOrigin.AutoCompletePlaces[0].PlaceId);

            //GooglePlaceAutoCompleteResult autoDest = await service.GetPlaces("אילת");
            //GooglePlace dest = await service.GetPlaceDetails(autoDest.AutoCompletePlaces[0].PlaceId);

            //GooglePlaceAutoCompleteResult autoPlace1 = await service.GetPlaces("באר שבע");
            //GooglePlace place1 = await service.GetPlaceDetails(autoPlace1.AutoCompletePlaces[0].PlaceId);
            //GooglePlaceAutoCompleteResult autoPlace2 = await service.GetPlaces("נתניה");
            //GooglePlace place2 = await service.GetPlaceDetails(autoPlace2.AutoCompletePlaces[0].PlaceId);

            //places.Add(place1);
            //places.Add(place2);

            //GoogleDirection directions = await service.GetDirectionsMulti($"{origin.Latitude}", $"{origin.Longitude}", $"{dest.Latitude}", $"{dest.Longitude}", places);



            //Clear all routes and pins from the map
            map.MapElements.Clear();

            ShowMapViewModel vm = (ShowMapViewModel)this.BindingContext;

            //Create two pins for origin and destination and add them to the map
            Pin startPin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(vm.RouteOrigin.Latitude, vm.RouteOrigin.Longitude),
                Label = "התחלה",
                Address = vm.RouteOrigin.Name
            };
            map.Pins.Add(startPin);

            Pin endPin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(vm.RouteDestination.Latitude, vm.RouteDestination.Longitude),
                Label = "יעד",
                Address = vm.RouteDestination.Name
            };
            map.Pins.Add(endPin);

            //Create pins for waypoints and add them to the map

            for (int i = 0; i < vm.RouteWaypoints.Count && i < vm.KidList.Count; i++)
            {
                GooglePlace route = vm.RouteWaypoints[i];

                Kid kid = vm.KidList[i];

                Pin pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(route.Latitude, route.Longitude),
                    Label = $"{kid.IdNavigation.FirstName} {kid.IdNavigation.LastName}",
                    Address = route.Name
                };
                map.Pins.Add(pin);
            }

            //foreach (GooglePlace route in vm.RouteWaypoints)
            //{
            //    Pin pin = new Pin
            //    {
            //        Type = PinType.Place,
            //        Position = new Position(route.Latitude, route.Longitude),
            //        Label = "נקודת איסוף",
            //        Address = route.Name
            //    };
            //    map.Pins.Add(pin);
            //}

            //Move the map to show the environment of the origin place! with radius of 5 KM... should be changed
            //according to the specific needs
            MapSpan span = MapSpan.FromCenterAndRadius(startPin.Position, Distance.FromKilometers(5));
            map.MoveToRegion(span);

            //Create the polyline between origin and destination
            //GoogleDirection directions = vm.RouteDirections;
            Xamarin.Forms.Maps.Polyline path = new Xamarin.Forms.Maps.Polyline()
            {
                StrokeColor = Xamarin.Forms.Color.FromHex("87D6C5"),
                StrokeWidth = 15
            };
            //run through each leg of the route, then, through each step
            foreach (Leg leg in vm.RouteDirections.Routes[0].Legs)
            {
                foreach (Step step in leg.Steps)
                {
                    var p = step.Polyline;
                    //Decode all positions of the line in this specific step!
                    IEnumerable<Position> positions = PolylineHelper.Decode(p.Points);

                    //Add the positions to the line
                    foreach (Position pos in positions)
                    {
                        path.Geopath.Add(pos);
                    }

                }
            }
            //Add the line to the map!
            map.MapElements.Add(path);
        }
    }
}