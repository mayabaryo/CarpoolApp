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


        #region AddCarpoolCommand
        public ICommand AddCarpoolCommand => new Command<Activity>(OnAddCarpool);
        public async void OnAddCarpool(Activity activity)
        {
            Page page = new AddCarpool();
            page.Title = "צור הסעה";
            await App.Current.MainPage.Navigation.PushAsync(page);

            Page p = new AddCarpool();

            AddCarpoolViewModel activityContext = new AddCarpoolViewModel()
            {
                Activity = activity
            };
            p.BindingContext = activityContext;
        }
        #endregion

        #region ActivitiesCommand
        public ICommand ShowCarpoolsCommand => new Command<Kid>(OnShowCarpools);
        public async void OnShowCarpools(Kid kid)
        {
            App theApp = (App)App.Current;
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<Activity> activities = await proxy.GetAllActivitiesAsync(kid);
            ObservableCollection<Activity> theActivities = new ObservableCollection<Activity>(activities);

            Page page = new ActivitiesPage();

            ActivitiesPageViewModel activityContext = new ActivitiesPageViewModel()
            {
                ActivityList = theActivities
            };
            page.BindingContext = activityContext;
            page.Title = $"{kid.IdNavigation.UserName} הפעילויות של";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}
