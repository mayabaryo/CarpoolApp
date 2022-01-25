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
    class ActivitiesPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Kid Kid { get; set; }
        public ObservableCollection<Activity> ActivityList { get; set; }


        public ActivitiesPageViewModel()
        {
            //ActivityList = new ObservableCollection<Activity>();
            //CreateActivityCollection();
        }

        #region CreateActivityCollection
        async void CreateActivityCollection()
        {
            if (Kid != null)
            {
                App theApp = (App)App.Current;
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                List<Activity> theActivities = await proxy.GetAllActivitiesAsync(Kid);
                foreach (Activity a in theActivities)
                {
                    this.ActivityList.Add(a);
                }
            }            
        }
        #endregion
    }
}
