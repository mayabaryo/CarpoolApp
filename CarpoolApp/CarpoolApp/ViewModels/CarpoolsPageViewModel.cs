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
    class CarpoolsPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Kid Kid { get; set; }
        public ObservableCollection<Carpool> CarpoolList { get; set; }
        public Activity Activity { get; set; }
        public ObservableCollection<Carpool> OtherCarpools { get; set; }
        public Carpool MyCarpool { get; set; }
        public bool ShowCarpool { get; set; }
        public bool ShowLabel { get => !ShowCarpool; }

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

        public CarpoolsPageViewModel()
        {

        }

        #region RequestToJoinCommand
        public ICommand RequestToJoinCommand => new Command<Carpool>(OnRequestToJoin);
        public async void OnRequestToJoin(Carpool carpool)
        {
            App theApp = (App)App.Current;

            RequestToJoinCarpool request = new RequestToJoinCarpool()
            {
                KidId = Kid.Id,
                CarpoolId = carpool.Id,
                //RequestStatusId = 3
            };

            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            bool addRequest = await proxy.AddRequestToJoinCarpoolAsync(request);

            if (!addRequest)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הגשת הבקשה להצטרפות להסעה נכשלה!", "בסדר", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                ServerStatus = "קורא נתונים...";


                //theApp.CurrentPlayer.RequestToJoinTeams.Add(request);

                await App.Current.MainPage.DisplayAlert("הגשת בקשה להצטרפות להסעה", "הגשת הבקשה להצטרפות להסעה נשלחה לנהג!", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();

                //NavigationPage p = new NavigationPage(new GamesScores());
                //NavigationPage.SetHasNavigationBar(p, false);
                //await App.Current.MainPage.Navigation.PushAsync(p);
            }

            //Page page = new AddCarpool();

            //AddCarpoolViewModel carpoolContext = new AddCarpoolViewModel()
            //{
            //    Activity = activity,
            //    Kid = this.Kid
            //};
            //page.BindingContext = carpoolContext;
            //page.Title = "צור הסעה";
            //await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}
