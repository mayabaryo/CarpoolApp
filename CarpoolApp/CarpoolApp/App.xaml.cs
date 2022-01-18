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
        //public Adult CurrentAdult { get; set; }
        //public Kid CurrentKid { get; set; }
        public List<string> Cities { get; set; }
        public List<string> Streets { get; set; }

        public App()
        {
            Cities = new List<string>();
            Streets = new List<string>();

            InitializeComponent();
            CurrentUser = null;
            //CurrentAdult = null;
            //CurrentKid = null;

            Page p = new Views.Login();
            p.Title = "התחברות";
            MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };

            //MainPage = new NavigationPage(new Views.AdultMainTab());

            //bdb2ff
            //MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            this.Streets = await proxy.GetStreetsAsync();
            this.Cities = await proxy.GetCitiesAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
