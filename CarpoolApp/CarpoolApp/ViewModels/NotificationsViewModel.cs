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
    class NotificationsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region RequestsCollection
        private ObservableCollection<RequestToJoinCarpool> requestsCollection;
        public ObservableCollection<RequestToJoinCarpool> RequestsCollection
        {
            get
            {
                return this.requestsCollection;
            }
            set
            {
                if (this.requestsCollection != value)
                {
                    this.requestsCollection = value;
                    OnPropertyChanged("RequestsCollection");
                }
            }
        }
        #endregion

        #region Constructor
        public NotificationsViewModel()
        {
            LoadRequests();
        }
        #endregion

        #region LoadRequests
        async void LoadRequests()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            List<RequestToJoinCarpool> requests = await proxy.GetRequestsToJoinCarpoolAsync(theApp.CurrentUser.Adult);

            if (requests != null)
            {
                RequestsCollection = new ObservableCollection<RequestToJoinCarpool>(requests);
            }
        }
        #endregion
    }
}
