using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarpoolApp.Models;
using CarpoolApp.Views;
using System.Collections.Generic;
using CarpoolApp.Services;
using CarpoolApp.DTO;

[assembly: ExportFont("AmaticSC-Regular.ttf", Alias = "Amatic")]
[assembly: ExportFont("epilogue.extralight.ttf", Alias = "Epilogue-ExtraLight")]
[assembly: ExportFont("epilogue.regular.ttf", Alias = "Epilogue")]
[assembly: ExportFont("Raleway-ExtraLight.ttf", Alias = "Raleway-ExtraLight")]
[assembly: ExportFont("sweet-n-sticky.regular.ttf", Alias = "Sweet-N-Sticky")]
[assembly: ExportFont("Tangerine-Regular.ttf", Alias = "Tangerine")]

namespace CarpoolApp
{

    public class Constants
    {
        //Generate Google Api Key at: https://console.cloud.google.com/ for Places API, Directions API, Maps SDK For android!
        //Generate Bing Api Key at: https://www.bingmapsportal.com/
        public const string GoogleApiKey = "AIzaSyDODu_-vK1_6456joACCNCj5AZ7v32_6Dk";
        public const string BingApiKey = "YOUR BING API KEY";
    }
    public partial class App : Application
    {
        public static bool IsDevEnv 
        { 
            get
            {
                return true;
            }
        }

        //The current logged in user
        public User CurrentUser { get; set; }
        public List<string> Cities { get; set; }
        public List<Street> StreetList { get; set; }


        public App()
        {
            Cities = new List<string>();
            StreetList = new List<Street>();

            InitializeComponent();
            //Set up google map api key
            GoogleMapsApiService.Initialize(Constants.GoogleApiKey);

            CurrentUser = null;


            Page p = new Views.Login();
            p.Title = "התחברות";
            MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };
        }

        protected async override void OnStart()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            this.Cities = await proxy.GetCitiesAsync();
            this.StreetList = await proxy.GetStreetListAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
