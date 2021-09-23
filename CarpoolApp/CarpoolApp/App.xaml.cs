using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public App()
        {
            InitializeComponent();

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
