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

namespace CarpoolApp.ViewModels
{
    class UpdateUserViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region FirstName
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion

        #region LastName
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
        }
        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                if (this.Password.Length < 6)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
                }
            }
            else
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region UserName
        private bool showUserNameError;

        public bool ShowUserNameError
        {
            get => showUserNameError;
            set
            {
                showUserNameError = value;
                OnPropertyChanged("ShowUserNameError");
            }
        }

        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                ValidateUserName();
                OnPropertyChanged("UserName");
            }
        }

        private string userNameError;

        public string UserNameError
        {
            get => userNameError;
            set
            {
                userNameError = value;
                OnPropertyChanged("UserNameError");
            }
        }

        private void ValidateUserName()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(UserName);
        }
        #endregion

        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region PhoneNum
        private bool showPhoneNumError;

        public bool ShowPhoneNumError
        {
            get => showPhoneNumError;
            set
            {
                showPhoneNumError = value;
                OnPropertyChanged("ShowPhoneNumError");
            }
        }

        private string phoneNum;

        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                ValidatePhoneNum();
                OnPropertyChanged("PhoneNum");
            }
        }

        private string phoneNumError;

        public string PhoneNumError
        {
            get => phoneNumError;
            set
            {
                phoneNumError = value;
                OnPropertyChanged("PhoneNumError");
            }
        }

        private void ValidatePhoneNum()
        {
            this.ShowPhoneNumError = string.IsNullOrEmpty(PhoneNum);
            if (!this.ShowPhoneNumError)
            {
                if (!Regex.IsMatch(this.PhoneNum, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                {
                    this.ShowPhoneNumError = true;
                    this.PhoneNumError = ERROR_MESSAGES.BAD_PHONE;
                }
            }
            else
                this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region BirthDate
        private bool showBirthDateError;

        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                ValidateBirthDate();
                OnPropertyChanged("BirthDate");
            }
        }

        private string birthDateError;

        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private const int ADULT_MIN_AGE = 18;
        private const int KID_MIN_AGE = 5;
        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.BirthDate;
            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;
            if (currentUser.Kid == null)
                this.ShowBirthDateError = ts.TotalDays < (ADULT_MIN_AGE * 365);
            else
                this.ShowBirthDateError = ts.TotalDays < (KID_MIN_AGE * 365);
        }
        #endregion

        #region UserImgSrc
        private string userImgSrc;

        public string UserImgSrc
        {
            get => userImgSrc;
            set
            {
                userImgSrc = value;
                OnPropertyChanged("UserImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.jpg";
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

        private string city;

        public string City
        {
            get => city;
            set
            {
                city = value;
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
            this.ShowCityError = string.IsNullOrEmpty(City);
        }
        #endregion

        #region Neighborhood
        private bool showNeighborhoodError;

        public bool ShowNeighborhoodError
        {
            get => showNeighborhoodError;
            set
            {
                showNeighborhoodError = value;
                OnPropertyChanged("ShowNeighborhoodError");
            }
        }

        private string neighborhood;

        public string Neighborhood
        {
            get => neighborhood;
            set
            {
                neighborhood = value;
                ValidateNeighborhood();
                OnPropertyChanged("Neighborhood");
            }
        }

        private string neighborhoodError;

        public string NeighborhoodError
        {
            get => neighborhoodError;
            set
            {
                neighborhoodError = value;
                OnPropertyChanged("NeighborhoodError");
            }
        }

        private void ValidateNeighborhood()
        {
            this.ShowNeighborhoodError = string.IsNullOrEmpty(Neighborhood);
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

        private string street;

        public string Street
        {
            get => street;
            set
            {
                street = value;
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
            this.ShowStreetError = string.IsNullOrEmpty(Street);
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

        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        #endregion

        ////set the user default photo image name
        //public const string DEFAULT_PHOTO = "defaultphoto.jpg";

        //private Adult theAdult;

        public Command SaveDataCommand { protected set; get; }
        public ICommand ClearCommand { protected set; get; }
        public ICommand HomePageCommand { protected set; get; }
        public ICommand AddKidPageCommand { protected set; get; }
        public ICommand AddAdultPageCommand { protected set; get; }

        public ICommand LogOutCommand { protected set; get; }

        #region Constructor
        public UpdateUserViewModel()
        {
            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            this.Email = currentUser.Email;
            this.UserName = currentUser.UserName;
            this.Password = currentUser.UserPswd;
            this.Name = currentUser.FirstName;
            this.LastName = currentUser.LastName;
            this.BirthDate = currentUser.BirthDate;
            this.PhoneNum = currentUser.PhoneNum;
            this.City = currentUser.City;
            this.Neighborhood = currentUser.Neighborhood;
            this.Street = currentUser.Street;
            this.HouseNum = currentUser.HouseNum;
            this.StringHouseNum = HouseNum.ToString();

            //set the path url to the contact photo
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            //Create a source with cache busting!
            Random r = new Random();
            this.UserImgSrc = currentUser.PhotoURL + $"?{r.Next()}";

            this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
            this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
            this.NameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.BAD_DATE;
            this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NeighborhoodError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.StringHouseNumError = ERROR_MESSAGES.BAD_HOUSE_NUM;

            this.ShowEmailError = false;
            this.ShowUserNameError = false;
            this.ShowPasswordError = false;
            this.ShowNameError = false;
            this.ShowLastNameError = false;
            this.ShowBirthDateError = false;
            this.ShowPhoneNumError = false;
            this.ShowCityError = false;
            this.ShowNeighborhoodError = false;
            this.ShowStreetError = false;
            this.ShowStringHouseNumError = false;

            this.SaveDataCommand = new Command(() => SaveData());
            ClearCommand = new Command(OnClear);
            HomePageCommand = new Command(OnHome);
            AddKidPageCommand = new Command(OnAddKid);
            AddAdultPageCommand = new Command(OnAddAdult);
            LogOutCommand = new Command(OnLogOut);
        }
        #endregion

        #region ValidateForm
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidatePassword();
            ValidateName();
            ValidateLastName();
            ValidateBirthDate();
            ValidatePhoneNum();
            ValidateCity();
            ValidateNeighborhood();
            ValidateStreet();
            ValidateStringHouseNum();

            //check if any validation failed
            if (ShowPasswordError || ShowNameError || ShowLastNameError
                || ShowBirthDateError || ShowPhoneNumError || ShowCityError
                || ShowNeighborhoodError || ShowStreetError || ShowStringHouseNumError)
                return false;
            return true;
        }
        #endregion

        #region SaveData
        private async void SaveData()
        {
            if (ValidateForm())
            {
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

                App theApp = (App)App.Current;
                User newUser = new User()
                {
                    Id = theApp.CurrentUser.Id,
                    Email = this.Email,
                    UserName = this.UserName,
                    UserPswd = this.Password,
                    FirstName = this.Name,
                    LastName = this.LastName,
                    BirthDate = this.BirthDate,
                    PhoneNum = this.PhoneNum,
                    City = this.City,
                    Neighborhood = this.Neighborhood,
                    Street = this.Street,
                    HouseNum = int.Parse(this.StringHouseNum)
                };

                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                User user = await proxy.UpdateUser(newUser);

                if (user == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "העדכון נכשל", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new Models.FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{user.Id}.jpg");

                        //if (!success)
                        //{
                        //    if (SetImageSourceEvent != null)
                        //        SetImageSourceEvent(theApp.CurrentUser.PhotoURL);
                        //    await App.Current.MainPage.DisplayAlert("עדכון", "יש בעיה בהעלאת התמונה", "אישור", FlowDirection.RightToLeft);
                        //}
                    }
                    ServerStatus = "שומר נתונים...";

                    theApp.CurrentUser = user;
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    Page page = new AdultMainTab();
                    page.Title = $"שלום {user.UserName}";
                    App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };
                    await App.Current.MainPage.DisplayAlert("עדכון", "העדכון בוצע בהצלחה", "אישור", FlowDirection.RightToLeft);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }
        #endregion

        #region RefreshCommand
        public ICommand RefreshCommand => new Command(OnRefresh);

        public void OnRefresh()
        {
            IsRefreshing = true;

            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            //this.UserImgSrc = currentUser.PhotoURL;
            this.Password = currentUser.UserPswd;
            this.Name = currentUser.FirstName;
            this.LastName = currentUser.LastName;
            this.BirthDate = currentUser.BirthDate;
            this.PhoneNum = currentUser.PhoneNum;
            this.City = currentUser.City;
            this.Neighborhood = currentUser.Neighborhood;
            this.Street = currentUser.Street;
            this.HouseNum = currentUser.HouseNum;
            this.StringHouseNum = HouseNum.ToString();
            if (SetImageSourceEvent != null)
                SetImageSourceEvent(currentUser.PhotoURL);

            IsRefreshing = false;
        }
        #endregion

        #region OnClear
        public void OnClear()
        {
            App theApp = (App)App.Current;
            User currentUser = theApp.CurrentUser;

            this.UserImgSrc = currentUser.PhotoURL;
            this.Password = currentUser.UserPswd;
            this.Name = currentUser.FirstName;
            this.LastName = currentUser.LastName;
            this.BirthDate = currentUser.BirthDate;
            this.PhoneNum = currentUser.PhoneNum;
            this.City = currentUser.City;
            this.Neighborhood = currentUser.Neighborhood;
            this.Street = currentUser.Street;
            this.HouseNum = currentUser.HouseNum;
            this.StringHouseNum = HouseNum.ToString();
            //if (SetImageSourceEvent != null)
            //    SetImageSourceEvent(currentUser.PhotoURL);
        }
        #endregion

        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        #region PickImage
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "בחר תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion

        //The following command handle the take photo button
        #region CameraImage
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "צלם תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }
        #endregion

        #region OnHome
        public async void OnHome()
        {
            App theApp = (App)App.Current;
            Page page = new AdultMainTab();
            page.Title = $"שלום {theApp.CurrentUser.UserName}";

            await App.Current.MainPage.Navigation.PopToRootAsync();
            //while (App.Current. != App.Current.MainPage)
            //await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region OnAddKid
        public async void OnAddKid()
        {
            AddKid page = new AddKid();
            page.Title = "הוסף ילד";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region OnAddAdult
        public async void OnAddAdult()
        {
            AddAdult page = new AddAdult();
            page.Title = "הוסף מבוגר";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region OnLogOut
        public async void OnLogOut()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("התנתקות", "האם ברצונך להתנתק?", "התנתק", "ביטול", FlowDirection.RightToLeft);
            if (answer)
            {
                App theApp = (App)App.Current;
                theApp.CurrentUser = null;

                Page page = new Login();
                page.Title = "התחברות";
                App.Current.MainPage = new NavigationPage(page) { BarBackgroundColor = Color.FromHex("#81cfe0") };
            }
        }
        #endregion
    }
}
