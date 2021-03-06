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
using System.Diagnostics;
using System.IO;
using CarpoolApp.DTO;
using System.Collections.ObjectModel;
using Org.BouncyCastle.Asn1.Cms;

namespace CarpoolApp.ViewModels
{
    class AddActivityViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private List<string> allCities;
        #region FilteredCities
        private ObservableCollection<string> filteredCities;
        public ObservableCollection<string> FilteredCities
        {
            get
            {
                return this.filteredCities;
            }
            set
            {
                if (this.filteredCities != value)
                {

                    this.filteredCities = value;
                    OnPropertyChanged("FilteredCities");
                }
            }
        }
        #endregion

        private List<Street> allStreets;
        #region FilteredStreets
        private ObservableCollection<string> filteredStreets;
        public ObservableCollection<string> FilteredStreets
        {
            get
            {
                return this.filteredStreets;
            }
            set
            {
                if (this.filteredStreets != value)
                {

                    this.filteredStreets = value;
                    OnPropertyChanged("FilteredStreets");
                }
            }
        }
        #endregion

        #region IsStreetEnabled
        private bool isStreetEnabled;
        public bool IsStreetEnabled
        {
            get => isStreetEnabled;
            set
            {
                isStreetEnabled = value;
                OnPropertyChanged("IsStreetEnabled");
            }
        }
        #endregion

        #region ActivityName
        private bool showActivityNameError;
        public bool ShowActivityNameError
        {
            get => showActivityNameError;
            set
            {
                showActivityNameError = value;
                OnPropertyChanged("showActivityNameError");
            }
        }

        private string activityName;
        public string ActivityName
        {
            get => activityName;
            set
            {
                activityName = value;
                ValidateActivityName();
                OnPropertyChanged("ActivityName");
            }
        }

        private string activityNameError;
        public string ActivityNameError
        {
            get => activityNameError;
            set
            {
                activityNameError = value;
                OnPropertyChanged("ActivityNameError");
            }
        }

        private void ValidateActivityName()
        {
            this.ShowActivityNameError = string.IsNullOrEmpty(ActivityName);
        }
        #endregion

        #region Date
        private bool showDateError;
        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                ValidateDate();
                OnPropertyChanged("Date");
            }
        }

        private string dateError;
        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }

        private void ValidateDate()
        {
            TimeSpan ts = this.Date - DateTime.Today;
            this.ShowDateError = ts.TotalMinutes < 0;
        }
        #endregion        

        #region StartTime
        private bool showStartTimeError;
        public bool ShowStartTimeError
        {
            get => showStartTimeError;
            set
            {
                showStartTimeError = value;
                OnPropertyChanged("ShowStartTimeError");
            }
        }

        private TimeSpan startTime;
        public TimeSpan StartTime
        {
            get => startTime;
            set
            {
                startTime = value;
                ValidateStartTime();
                OnPropertyChanged("StartTime");
            }
        }

        private string startTimeError;
        public string StartTimeError
        {
            get => startTimeError;
            set
            {
                startTimeError = value;
                OnPropertyChanged("StartTimeError");
            }
        }

        private void ValidateStartTime()
        {
            TimeSpan span = this.EndTime - this.StartTime;
            this.ShowEndTimeError = span.TotalMinutes < 0;
        }
        #endregion        

        #region EndTime
        private bool showEndTimeError;
        public bool ShowEndTimeError
        {
            get => showEndTimeError;
            set
            {
                showEndTimeError = value;
                OnPropertyChanged("ShowEndTimeError");
            }
        }

        private TimeSpan endTime;
        public TimeSpan EndTime
        {
            get => endTime;
            set
            {
                endTime = value;
                ValidateEndTime();
                OnPropertyChanged("EndTime");
            }
        }

        private string endTimeError;
        public string EndTimeError
        {
            get => endTimeError;
            set
            {
                endTimeError = value;
                OnPropertyChanged("EndTimeError");
            }
        }

        private void ValidateEndTime()
        {
            TimeSpan span = this.EndTime - this.StartTime;
            this.ShowEndTimeError = span.TotalMinutes < 0;
        }
        #endregion        

        #region City
        private bool showCityError;
        public bool ShowCityError
        {
            get => showCityError;
            set
            {
                showCityError = value;
                OnPropertyChanged("ShowCityError");
            }
        }

        //This property holds the selected city on the collection of cities
        private string selectedCityItem;
        public string SelectedCityItem
        {
            get => selectedCityItem;
            set
            {
                selectedCityItem = value;
                OnPropertyChanged("SelectedCityItem");
            }
        }

        //ShowCities
        private bool showCities;
        public bool ShowCities
        {
            get => showCities;
            set
            {
                showCities = value;
                OnPropertyChanged("ShowCities");
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnCityChanged(value);
                ValidateCity();
                OnPropertyChanged("City");
            }
        }

        private string cityError;
        public string CityError
        {
            get => cityError;
            set
            {
                cityError = value;
                OnPropertyChanged("CityError");
            }
        }

        private void ValidateCity()
        {
            this.ShowCityError = string.IsNullOrEmpty(this.City);
            if (!this.ShowCityError)
            {
                string city = this.allCities.Where(c => c == this.City).FirstOrDefault();
                if (string.IsNullOrEmpty(city))
                {
                    this.ShowCityError = true;
                    this.CityError = ERROR_MESSAGES.BAD_CITY;
                }
            }
            else
                this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Street
        private bool showStreetError;
        public bool ShowStreetError
        {
            get => showStreetError;
            set
            {
                showStreetError = value;
                OnPropertyChanged("ShowStreetError");
            }
        }

        //This property holds the selected street on the collection of streets
        private string selectedStreetItem;
        public string SelectedStreetItem
        {
            get => selectedStreetItem;
            set
            {
                selectedStreetItem = value;
                OnPropertyChanged("SelectedStreetItem");
            }
        }

        //ShowStreets
        private bool showStreets;
        public bool ShowStreets
        {
            get => showStreets;
            set
            {
                showStreets = value;
                OnPropertyChanged("ShowStreets");
            }
        }

        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnStreetChanged(value);
                ValidateStreet();
                OnPropertyChanged("Street");
            }
        }

        private string streetError;
        public string StreetError
        {
            get => streetError;
            set
            {
                streetError = value;
                OnPropertyChanged("StreetError");
            }
        }

        private void ValidateStreet()
        {
            this.ShowStreetError = string.IsNullOrEmpty(this.Street);
            if (!this.ShowStreetError)
            {
                Street street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
                if (street == null)
                {
                    this.ShowStreetError = true;
                    this.StreetError = ERROR_MESSAGES.BAD_STREET;
                }
            }
            else
                this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region HouseNum
        private bool showHouseNumError;
        public bool ShowHouseNumError
        {
            get => showHouseNumError;
            set
            {
                showHouseNumError = value;
                OnPropertyChanged("ShowHouseNumError");
            }
        }

        private int houseNum;
        public int HouseNum
        {
            get => houseNum;
            set
            {
                houseNum = value;
                ValidateHouseNum();
                OnPropertyChanged("HouseNum");
            }
        }

        private string houseNumError;
        public string HouseNumError
        {
            get => houseNumError;
            set
            {
                houseNumError = value;
                OnPropertyChanged("HouseNumError");
            }
        }

        private void ValidateHouseNum()
        {
            this.ShowHouseNumError = this.HouseNum == 0;
            int i;
            string num = this.HouseNum.ToString();
            if (!this.ShowHouseNumError)
            {
                if (!int.TryParse(num, out i) || int.Parse(num) <= 0/*!Regex.IsMatch(num, @"^[-+]?[0-9]*\.?[0-9]+$")*/)
                {
                    this.ShowHouseNumError = true;
                    this.HouseNumError = ERROR_MESSAGES.BAD_HOUSE_NUM;
                }
            }
            else
                this.HouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region StringHouseNum
        private bool showStringHouseNumError;
        public bool ShowStringHouseNumError
        {
            get => showStringHouseNumError;
            set
            {
                showStringHouseNumError = value;
                OnPropertyChanged("ShowStringHouseNumError");
            }
        }

        private string stringHouseNum;
        public string StringHouseNum
        {
            get => stringHouseNum;
            set
            {
                stringHouseNum = value;
                ValidateStringHouseNum();
                OnPropertyChanged("StringHouseNum");
            }
        }

        private string stringHouseNumError;
        public string StringHouseNumError
        {
            get => stringHouseNumError;
            set
            {
                stringHouseNumError = value;
                OnPropertyChanged("StringHouseNumError");
            }
        }

        private void ValidateStringHouseNum()
        {
            this.ShowStringHouseNumError = string.IsNullOrEmpty(this.StringHouseNum);
            int i;
            if (!this.ShowStringHouseNumError)
            {
                if (!int.TryParse(this.StringHouseNum, out i) || int.Parse(this.StringHouseNum) <= 0 /*!Regex.IsMatch(this.StringHouseNum, @"^[-+]?[0-9]*\.?[0-9]+$")*/)
                {
                    this.ShowStringHouseNumError = true;
                    this.StringHouseNumError = ERROR_MESSAGES.BAD_HOUSE_NUM;
                }
            }
            else
                this.StringHouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;
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
        public AddActivityViewModel()
        {
            App theApp = (App)App.Current;

            this.allCities = theApp.Cities;
            this.FilteredCities = new ObservableCollection<string>();

            this.allStreets = theApp.StreetList;
            this.FilteredStreets = new ObservableCollection<string>();
            this.IsStreetEnabled = false;

            this.ActivityNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.DateError = ERROR_MESSAGES.BAD_ACTIVITY_DATE;
            //this.StartTimeError = ERROR_MESSAGES.BAD_ACTIVITY_DATE;
            this.EndTimeError = ERROR_MESSAGES.BAD_END_TIME;
            this.CityError = ERROR_MESSAGES.BAD_CITY;
            this.StreetError = ERROR_MESSAGES.BAD_STREET;
            this.StringHouseNumError = ERROR_MESSAGES.BAD_HOUSE_NUM;
            //this.EntryCodeError = ERROR_MESSAGES.SHORT_PASS;

            this.ShowActivityNameError = false;
            this.ShowDateError = false;
            //this.ShowStartTimeError = false;
            this.ShowEndTimeError = false;
            this.ShowCityError = false;
            this.ShowStreetError = false;
            this.ShowStringHouseNumError = false;
            //this.ShowEntryCodeError = false;

            this.Date = DateTime.Today;
            //this.StartTime = DateTime.Now;
            //this.EndTime = DateTime.Now;
        }
        #endregion

        //This function validate the entire form upon submit!
        #region ValidateForm
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateActivityName();
            ValidateDate();
            ValidateStartTime();
            ValidateEndTime();
            ValidateCity();
            ValidateStreet();
            ValidateStringHouseNum();
            //ValidateEntryCode();

            //check if any validation failed
            if (ShowActivityNameError || ShowDateError || ShowStartTimeError || ShowEndTimeError || ShowCityError
                || ShowStreetError || ShowStringHouseNumError/* || ShowEntryCodeError*/)
                return false;
            return true;
        }
        #endregion

        #region SaveData
        //public Command SaveDataCommand { protected set; get; }
        public Command SaveDataCommand => new Command(SaveData);
        private async void SaveData()
        {
            if (ValidateForm())
            {
                App theApp = (App)App.Current;
                User currentUser = theApp.CurrentUser;

                DateTime start = this.Date + this.StartTime;
                DateTime end = this.Date + this.EndTime;

                Models.Activity activity = new Models.Activity()
                {
                    ActivityName = this.ActivityName,
                    StartTime = start,
                    EndTime = end,
                    City = this.City,
                    Street = this.Street,
                    HouseNum = int.Parse(this.StringHouseNum),
                    AdultId = currentUser.Id
                };

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();

                Models.Activity newActivity = await proxy.AddActivityAsync(activity);
                if (newActivity == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הוספת הפעילות נכשלה", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    ServerStatus = "שולח קוד...";

                    string activityCode = ActivityCode.CreateGroupCode(newActivity.Id);
                    string body = "תודה שיצרת פעילות חדשה! קוד הכניסה לפעילות שלך הינו " + activityCode;
                    string to = currentUser.Email;
                    string toName = currentUser.UserName;
                    bool isSent = await proxy.SendEmailAsync(body, to, toName);

                    ServerStatus = "שומר נתונים...";

                    Page page = new AdultMainTab();
                    page.Title = "שלום " + theApp.CurrentUser.UserName;
                    theApp.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                    await App.Current.MainPage.DisplayAlert("הוספה", "הוספת הפעילות בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                }                
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }
        #endregion

        #region OnCityChanged
        public void OnCityChanged(string search)
        {
            this.Street = "";
            this.ShowStreets = false;
            this.FilteredStreets.Clear();
            this.IsStreetEnabled = false;

            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }
            //Filter the list of cities based on the search term
            if (this.allCities == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                foreach (string city in this.allCities)
                {
                    if (!this.FilteredCities.Contains(city) &&
                        city.Contains(search))
                        this.FilteredCities.Add(city);
                    else if (this.FilteredCities.Contains(city) &&
                        !city.Contains(search))
                        this.FilteredCities.Remove(city);
                }
            }
        }
        #endregion

        #region OnStreetChanged
        public void OnStreetChanged(string search)
        {
            if (this.Street != this.SelectedStreetItem)
            {
                this.ShowStreets = true;
                this.SelectedStreetItem = null;
            }
            //Filter the list of streets based on the search term
            if (this.allStreets == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowStreets = false;
                this.FilteredStreets.Clear();
            }
            else
            {
                foreach (Street street in this.allStreets)
                {
                    string streetName = street.street_name;

                    if (!this.FilteredStreets.Contains(streetName) &&
                        streetName.Contains(search) && street.city_name == this.City)
                        this.FilteredStreets.Add(streetName);
                    else if (this.FilteredStreets.Contains(streetName) &&
                        (!streetName.Contains(search) || !(street.city_name == this.City)))
                        this.FilteredStreets.Remove(streetName);
                }
            }
        }
        #endregion

        #region SelectedCity
        public ICommand SelectedCity => new Command<string>(OnSelectedCity);
        public void OnSelectedCity(string city)
        {
            if (city != null)
            {
                this.ShowCities = false;
                this.City = city;

                this.IsStreetEnabled = true;
            }
        }
        #endregion

        #region SelectedStreet
        public ICommand SelectedStreet => new Command<string>(OnSelectedStreet);
        public void OnSelectedStreet(string street)
        {
            if (street != null)
            {
                this.ShowStreets = false;
                this.Street = street;
            }
        }
        #endregion
    }
}
