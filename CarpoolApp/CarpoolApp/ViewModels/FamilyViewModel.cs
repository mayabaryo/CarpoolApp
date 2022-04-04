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
    class FamilyViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ActivityCode
        private string activityCode;
        public string ActivityCode
        {
            get { return activityCode; }
            set
            {
                activityCode = value;
                OnPropertyChanged("ActivityCode");
            }
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

        #region KidsCollection
        private ObservableCollection<Kid> kidsCollection;
        public ObservableCollection<Kid> KidsCollection
        {
            get
            {
                return this.kidsCollection;
            }
            set
            {
                if (this.kidsCollection != value)
                {
                    this.kidsCollection = value;
                    OnPropertyChanged("KidsCollection");
                }
            }
        }
        #endregion

        public ObservableCollection<Kid> KidList { get; }

        #region Constructor
        public FamilyViewModel()
        {
            KidList = new ObservableCollection<Kid>();
            CreateKidCollection();
        }
        #endregion

        #region CreateKidCollection
        async void CreateKidCollection()
        {
            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;
            //Adult currentAdult = new Adult { IdNavigation = currentUser };

            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<Kid> theKids = await proxy.GetAllKidsAsync(currentUser.Adult);

            //if (theKids != null)
            //{
            //    KidsCollection = new ObservableCollection<Kid>(theKids);
            //}

            //this.KidList = new ObservableCollection<Kid>(theKids);

            foreach (Kid k in theKids)
            {
                this.KidList.Add(k);
            }
        }
        #endregion

        #region JoinToActivityCommand
        public ICommand JoinToActivityCommand => new Command<Kid>(OnJoinToActivity);
        public async void OnJoinToActivity(Kid kid)
        {
            //string code = Models.ActivityCode.CreateGroupCode(1);
            //ActivityCode = code;

            int activityId = Models.ActivityCode.CodeToGroupID(ActivityCode);

            KidsInActivity kidInActivity = new KidsInActivity()
            {
                ActivityId = activityId,
                KidId = kid.Id
            };
            
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            bool isActivityExist = await proxy.ActivityExistAsync(activityId);
            if (isActivityExist)
            {
                KidsInActivity newKidsIn = await proxy.JoinToActivityAsync(kidInActivity);

                if (newKidsIn == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "ההצטרפות נכשלה", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    
                    ServerStatus = "שומר נתונים...";

                    //App theApp = (App)App.Current;

                    //Page p = new AdultMainTab();
                    //p.Title = $"שלום {theApp.CurrentUser.UserName}";
                    //theApp.MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                    await App.Current.MainPage.DisplayAlert("הצטרפות", "ההצטרפות בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                }

                //AddKid page = new AddKid();
                //page.Title = "הוסף ילד";
                //await App.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "הפעילות אינה קיימת במערכת", "אישור", FlowDirection.RightToLeft);
            }     
        }
        #endregion

        #region ActivitiesCommand
        public ICommand ActivitiesCommand => new Command<Kid>(OnActivities);
        public async void OnActivities(Kid kid)
        {
            App theApp = (App)App.Current;
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            List<Activity> activities = await proxy.GetKidActivitiesAsync(kid);
            ObservableCollection<Activity> theActivities = new ObservableCollection<Activity>(activities);

            Page page = new ActivitiesPage();

            ActivitiesPageViewModel activityContext = new ActivitiesPageViewModel()
            {
                Kid = kid,
                ActivityList = theActivities
            };
            page.BindingContext = activityContext;
            page.Title = $"{kid.IdNavigation.UserName} הפעילויות של";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region MoveToAddKid
        public ICommand MoveToAddKid => new Command(OnMoveToAddKid);
        public async void OnMoveToAddKid()
        {
            AddKid page = new AddKid();
            page.Title = "הוסף ילד";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region MoveToAddAdult
        public ICommand MoveToAddAdult => new Command(OnMoveToAddAdult);
        public async void OnMoveToAddAdult()
        {
            AddAdult page = new AddAdult();
            page.Title = "הוסף מבוגר";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}
