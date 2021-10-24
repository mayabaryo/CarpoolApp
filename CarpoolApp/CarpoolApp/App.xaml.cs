﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarpoolApp.Models;
using CarpoolApp.Views;
using System.Collections.Generic;

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
        public Adult CurrentAdult { get; set; }
        public Kid CurrentKid { get; set; }


        public App()
        {
            InitializeComponent();
            CurrentUser = null;
            CurrentAdult = null;
            CurrentKid = null;

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
