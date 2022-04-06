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

        #region RequestsCollection
        private ObservableCollection<KidsInCarpool> requestsCollection;
        public ObservableCollection<KidsInCarpool> RequestsCollection
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
            ApproveCommand = new Command<KidsInCarpool>(ApproveRequest);
            DeleteCommand = new Command<KidsInCarpool>(DeleteRequest);
        }
        #endregion

        #region LoadRequests
        async void LoadRequests()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            App theApp = (App)App.Current;

            List<KidsInCarpool> requests = await proxy.GetRequestsToJoinCarpoolAsync(theApp.CurrentUser.Adult);

            if (requests != null)
            {
                RequestsCollection = new ObservableCollection<KidsInCarpool>(requests);
            }
        }
        #endregion

        #region ApproveRequest
        public ICommand ApproveCommand { get; protected set; }
        private async void ApproveRequest(KidsInCarpool kidsIn)
        {
            App theApp = (App)App.Current;
            //Player player = request.Player;
            //player.Team = request.Team;

            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            bool approved = await proxy.ApproveRequestToJoinCarpoolAsync(kidsIn.KidId, kidsIn.CarpoolId);

            if (!approved)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת המשתמש להסעה נכשלה!", "בסדר");
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                //theApp.CurrentCoach.Team.Players.Add(player);
                ServerStatus = "קורא נתונים...";

                await App.Current.MainPage.DisplayAlert("אישור בקשת הצטרפות להסעה", "הוספת משתמש להסעה בוצעה בהצלחה!", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();

                Page page = new AdultMainTab();
                page.Title = "שלום " + theApp.CurrentUser.UserName;
                App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                //NavigationPage p = new NavigationPage(new GamesScores());
                //NavigationPage.SetHasNavigationBar(p, false);
                //await App.Current.MainPage.Navigation.PushAsync(p);
            }

        }
        #endregion

        #region DeleteRequest
        public ICommand DeleteCommand { get; protected set; }
        private async void DeleteRequest(KidsInCarpool kidsIn)
        {
            App theApp = (App)App.Current;
            //Player player = request.Player;
            //player.Team = null;

            ServerStatus = "מתחבר לשרת...";
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            bool deleted = await proxy.DeclineRequestToJoinCarpoolAsync(kidsIn.KidId, kidsIn.CarpoolId);

            if (!deleted)
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "דחיית בקשת השחקן להצטרפות לקבוצה נכשלה!", "בסדר");
            }
            else
            {
                ServerStatus = "קורא נתונים...";

                await App.Current.MainPage.DisplayAlert("התחברות", "דחיית בקשת השחקן להצטרפות לקבוצה בוצעה בהצלחה!", "אישור", FlowDirection.RightToLeft);
                await App.Current.MainPage.Navigation.PopModalAsync();

                Page page = new AdultMainTab();
                page.Title = "שלום " + theApp.CurrentUser.UserName;
                App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                //NavigationPage p = new NavigationPage(new GamesScores());
                //NavigationPage.SetHasNavigationBar(p, false);
                //await App.Current.MainPage.Navigation.PushAsync(p);
            }
        }
        #endregion
    }
}
