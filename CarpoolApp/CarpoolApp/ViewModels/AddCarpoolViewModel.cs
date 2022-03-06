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
    class AddCarpoolViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public Activity Activity { get; set; }
        public Kid Kid { get; set; }


        #region CarpoolTime
        private bool showCarpoolTimeError;
        public bool ShowCarpoolTimeError
        {
            get => showCarpoolTimeError;
            set
            {
                showCarpoolTimeError = value;
                OnPropertyChanged("ShowCarpoolTimeError");
            }
        }

        private DateTime carpoolTime;
        public DateTime CarpoolTime
        {
            get => carpoolTime;
            set
            {
                carpoolTime = value;
                ValidateCarpoolTime();
                OnPropertyChanged("CarpoolTime");
            }
        }

        private string carpoolTimeError;
        public string CarpoolTimeError
        {
            get => carpoolTimeError;
            set
            {
                carpoolTimeError = value;
                OnPropertyChanged("CarpoolTimeError");
            }
        }

        private void ValidateCarpoolTime()
        {
            TimeSpan ts = this.CarpoolTime - DateTime.Now;
            this.ShowCarpoolTimeError = ts.TotalMinutes < 0;
        }
        #endregion        

        #region StringSeats
        private bool showStringSeatsError;
        public bool ShowStringSeatsError
        {
            get => showStringSeatsError;
            set
            {
                showStringSeatsError = value;
                OnPropertyChanged("ShowStringSeatsError");
            }
        }

        private string stringSeats;
        public string StringSeats
        {
            get => stringSeats;
            set
            {
                stringSeats = value;
                ValidateStringSeats();
                OnPropertyChanged("StringSeats");
            }
        }

        private string stringSeatsError;
        public string StringSeatsError
        {
            get => stringSeatsError;
            set
            {
                stringSeatsError = value;
                OnPropertyChanged("StringHouseNumError");
            }
        }

        private void ValidateStringSeats()
        {
            this.ShowStringSeatsError = string.IsNullOrEmpty(this.StringSeats);
            int i;
            if (!this.ShowStringSeatsError)
            {
                if (!int.TryParse(this.StringSeats, out i) || int.Parse(this.StringSeats) <= 0 /*!Regex.IsMatch(this.StringHouseNum, @"^[-+]?[0-9]*\.?[0-9]+$")*/)
                {
                    this.ShowStringSeatsError = true;
                    this.StringSeatsError = ERROR_MESSAGES.BAD_SEATS;
                }
            }
            else
                this.StringSeatsError = ERROR_MESSAGES.REQUIRED_FIELD;
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

        #region Constructor
        public AddCarpoolViewModel()
        {
            this.CarpoolTimeError = ERROR_MESSAGES.BAD_ACTIVITY_DATE;
            this.StringSeatsError = ERROR_MESSAGES.BAD_SEATS;

            this.ShowCarpoolTimeError = false;
            this.ShowStringSeatsError = false;


            this.SaveDataCommand = new Command(() => SaveData());

            DateTime calendarDate = new DateTime(2000, 10, 10);
            this.CarpoolTime = DateTime.Now;
        }
        #endregion

        #region SaveData
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
            {
                App theApp = (App)App.Current;
                User currentUser = theApp.CurrentUser;

                Adult currentAdult = new Adult()
                {
                    IdNavigation = currentUser
                };

                Carpool carpool = new Carpool()
                {
                    AdultId = currentUser.Id,
                    CarpoolTime = this.CarpoolTime,
                    Seats = int.Parse(this.StringSeats),
                    CarpoolStatusId = 1,
                    ActivityId = this.Activity.Id
                };
               
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                Carpool newCarpool = await proxy.AddCarpoolAsync(carpool);

                if (newCarpool == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "ההוספה נכשלה", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    KidsInCarpool kidsInCarpool = new KidsInCarpool()
                    {
                        KidId = Kid.IdNavigation.Id,
                        CarpoolId = newCarpool.Id
                    };

                    //הוספת הילד הנוכחי להסעה
                    KidsInCarpool newKidsIn = await proxy.JoinToCarpoolAsync(kidsInCarpool);

                    ServerStatus = "שומר נתונים...";

                    Page page = new AdultMainTab();
                    page.Title = "שלום" + theApp.CurrentUser.UserName;
                    App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("ההוספה", "ההוספה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                }                
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }
        #endregion

        //This function validate the entire form upon submit!
        #region ValidateForm
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateCarpoolTime();
            ValidateStringSeats();

            //check if any validation failed
            if (ShowCarpoolTimeError || ShowStringSeatsError)
                return false;
            return true;
        }
        #endregion
    }
}
