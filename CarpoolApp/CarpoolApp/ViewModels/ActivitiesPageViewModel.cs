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
                List<Activity> theActivities = await proxy.GetKidActivitiesAsync(Kid);
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

            AddCarpoolViewModel carpoolContext = new AddCarpoolViewModel()
            {
                Activity = activity,
                Kid = this.Kid
            };
            page.BindingContext = carpoolContext;
            page.Title = "צור הסעה";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region CarpoolsCommand
        public ICommand CarpoolsCommand => new Command<Activity>(OnCarpools);
        public async void OnCarpools(Activity activity)
        {
            App theApp = (App)App.Current;
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<Carpool> kidCarpools = await proxy.GetKidCarpoolsAsync(this.Kid);

            ObservableCollection<Carpool> theCarpools = new ObservableCollection<Carpool>(kidCarpools);

            List<Carpool> activityCarpools = await proxy.GetCarpoolsInActivityAsync(activity);

            //System.Threading.Thread.Sleep(10000);

            //ObservableCollection<Carpool> carpoolsInAcivity = new ObservableCollection<Carpool>(activityCarpools);


            Carpool myCarpool = theCarpools.Where(a => a.ActivityId == activity.Id).FirstOrDefault();

            //List<Carpool> otherCarpools = activityCarpools;
            //if (myCarpool != null)
            //{
            //    foreach (Carpool c in activityCarpools)
            //    {
            //        if (c.Id == myCarpool.Id)
            //            activityCarpools.Remove(c);
            //    }
            //}


            Page page = new CarpoolsPage();
            CarpoolsPageViewModel carpoolContext = new CarpoolsPageViewModel()
            {
                Kid = this.Kid,
                CarpoolList = theCarpools,
                Activity = activity,
                OtherCarpools = new ObservableCollection<Carpool>(activityCarpools),
                MyCarpool = myCarpool,
                ShowCarpool = (myCarpool != null)
            };

            page.BindingContext = carpoolContext;
            page.Title = "הצטרף להסעה";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}
