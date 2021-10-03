using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarpoolApp.Models;
using CarpoolApp.Views;
using System.Collections.Generic;

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

        public App()
        {
            InitializeComponent();
            CurrentUser = null;

            Page p = new Views.Login();
            p.Title = "Login";
            MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };

            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
