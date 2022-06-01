using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using CarpoolApp.Services;
using CarpoolApp.Models;
using Xamarin.Essentials;
using System.Linq;
using CarpoolApp.Views;
using System.Text.RegularExpressions;
using CarpoolApp.DTO;
using System.Collections.ObjectModel;

namespace CarpoolApp.ViewModels
{
    class AllKidsCarpoolsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ObservableCollection<Carpool> CarpoolList { get; set; }

        #region ServerStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion

        #region Constructor
        public AllKidsCarpoolsViewModel()
        {
            CarpoolList = new ObservableCollection<Carpool>();
            CreateCarpoolCollection();
        }
        #endregion

        #region CreateCarpoolCollection
        async void CreateCarpoolCollection()
        {
            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<Carpool> theCarpools = await proxy.GetAllKidsCarpoolsAsync(currentUser.Adult);

            foreach (Carpool c in theCarpools)
            {
                this.CarpoolList.Add(c);
            }
        }
        #endregion

        #region StartCommand
        public ICommand StartCommand => new Command<Carpool>(OnStart);
        public async void OnStart(Carpool carpool)
        {
            App theApp = (App)App.Current;
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();

            User driver = carpool.Adult.IdNavigation;
            string origin = $"{driver.City},{driver.Street} {driver.HouseNum}";

            Activity activity = carpool.Activity;
            string dest = $"{activity.City},{activity.Street} {activity.HouseNum}";

            List<Kid> kids = await proxy.GetKidsInCarpoolAsync(carpool);

            List<string> waypoints = new List<string>();
            foreach (Kid kid in kids)
            {
                User user = kid.IdNavigation;
                string point = $"{user.City},{user.Street} {user.HouseNum}";
                waypoints.Add(point);
            }

            ShowMap page = new ShowMap(origin, dest, waypoints, kids, carpool);
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}
