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
    class AdultCarpoolsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ObservableCollection<Carpool> CarpoolList { get; }

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
        public AdultCarpoolsViewModel()
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
            Adult currentAdult = new Adult { IdNavigation = currentUser };

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<Carpool> theCarpools = await proxy.GetAdultCarpoolsAsync(currentAdult);
            foreach (Carpool c in theCarpools)
            {
                this.CarpoolList.Add(c);
            }
        }
        #endregion

    }
}
