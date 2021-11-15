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

namespace CarpoolApp.ViewModels
{
    //public static class ERROR_MESSAGES
    //{
    //    public const string REQUIRED_FIELD = "זהו שדה חובה";
    //    public const string BAD_EMAIL = "אימייל לא תקין";
    //    public const string SHORT_PASS = "הסיסמה חייבת להכיל לפחות 6 תווים";
    //    public const string BAD_PHONE = "טלפון לא תקין";
    //    public const string BAD_DATE = "עלייך להיות מעל גיל 18";
    //    public const string BAD_HOUSE_NUM = "מספר בית לא תקין";
    //}

    public class AddKidViewModel : INotifyPropertyChanged
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

        private const int MIN_AGE = 18;
        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.BirthDate;
            this.ShowBirthDateError = ts.TotalDays < (MIN_AGE * 365);
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


        //private User theUser;
        private Kid theKid;

        public AddKidViewModel(Kid kid = null)
        {
            //create a new user contact if this is an add operation
            if (kid == null)
            {
                App theApp = (App)App.Current;

                User u = new User()
                {
                    //Id = theApp.CurrentUser.Id,
                    Email = "",
                    UserName = "",
                    UserPswd = "",
                    FirstName = "",
                    LastName = "",
                    BirthDate = DateTime.Now,
                    PhoneNum = "",
                    City = "",
                    Neighborhood = "",
                    Street = "",
                    HouseNum = default
                };
                StringHouseNum = "";

                kid = new Kid()
                {
                    IdNavigation = u
                };

                //Setup default image photo
                this.UserImgSrc = DEFAULT_PHOTO_SRC;
                this.imageFileResult = null; //mark that no picture was chosen
            }
            else
            {
                //set the path url to the contact photo
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                //Create a source with cache busting!
                Random r = new Random();
                this.UserImgSrc = proxy.GetBasePhotoUri() + kid.Id + $".jpg?{r.Next()}";
            }


            this.theKid = kid;
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
            //this.HouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            //this.ShowHouseNumError = false;
            this.ShowStringHouseNumError = false;

            this.SaveDataCommand = new Command(() => SaveData());

            this.Email = kid.IdNavigation.Email;
            this.UserName = kid.IdNavigation.UserName;
            this.Password = kid.IdNavigation.UserPswd;
            this.Name = kid.IdNavigation.FirstName;
            this.LastName = kid.IdNavigation.LastName;
            this.BirthDate = kid.IdNavigation.BirthDate;
            this.PhoneNum = kid.IdNavigation.PhoneNum;
            this.City = kid.IdNavigation.City;
            this.Neighborhood = kid.IdNavigation.Neighborhood;
            this.Street = kid.IdNavigation.Street;
            this.HouseNum = kid.IdNavigation.HouseNum;
        }

        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateEmail();
            ValidateUserName();
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
            if (ShowEmailError || ShowUserNameError || ShowPasswordError || ShowNameError
                || ShowLastNameError || ShowBirthDateError || ShowPhoneNumError || ShowCityError
                || ShowNeighborhoodError || ShowStreetError || ShowStringHouseNumError)
                return false;
            return true;
        }

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

        //This event is fired after the new contact is generated in the system so it can be added to the list of contacts
        public event Action<Adult, Adult> ContactUpdatedEvent;

        //The command for saving the contact
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
            {
                this.theKid.IdNavigation.Photo = this.UserImgSrc;
                this.theKid.IdNavigation.Email = this.Email;
                this.theKid.IdNavigation.UserName = this.UserName;
                this.theKid.IdNavigation.UserPswd = this.Password;
                this.theKid.IdNavigation.FirstName = this.Name;
                this.theKid.IdNavigation.LastName = this.LastName;
                this.theKid.IdNavigation.BirthDate = this.BirthDate;
                this.theKid.IdNavigation.PhoneNum = this.PhoneNum;
                this.theKid.IdNavigation.City = this.City;
                this.theKid.IdNavigation.Neighborhood = this.Neighborhood;
                this.theKid.IdNavigation.Street = this.Street;
                this.theKid.IdNavigation.HouseNum = int.Parse(this.StringHouseNum);

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();

                bool isEmailExist = await proxy.EmailExistAsync(this.theKid.IdNavigation.Email);
                bool isUserNameExist = await proxy.UserNameExistAsync(this.theKid.IdNavigation.UserName);

                if (!isEmailExist && !isUserNameExist)
                {
                    Kid newKid = await proxy.KidSignUpAsync(this.theKid);
                    if (newKid == null)
                    {
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.DisplayAlert("שגיאה", "ההרשמה נכשלה", "אישור", FlowDirection.RightToLeft);
                    }
                    else
                    {
                        if (this.imageFileResult != null)
                        {
                            ServerStatus = "מעלה תמונה...";

                            bool success = await proxy.UploadImage(new FileInfo()
                            {
                                Name = this.imageFileResult.FullPath
                            }, $"{newKid.Id}.jpg");
                        }
                        ServerStatus = "שומר נתונים...";

                        

                        ////if someone registered to get the contact added event, fire the event
                        //if (this.ContactUpdatedEvent != null)
                        //{
                        //    this.ContactUpdatedEvent(newKid, this.theKid);
                        //}

                        //close the message and add contact windows!
                        await App.Current.MainPage.Navigation.PopAsync();

                        App theApp = (App)App.Current;
                        //theApp.CurrentUser = newKid.IdNavigation;

                        KidsOfAdult kidsOfAdult = new KidsOfAdult()
                        {
                            AdultId = theApp.CurrentUser.Id,
                            KidId = newKid.Id
                        };

                        //Page p = new AdultPage();
                        //p.Title = $"שלום {theApp.CurrentUser.UserName}";
                        //theApp.MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };

                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.DisplayAlert("הוספה", "ההוספה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                    }
                }
                else
                {
                    if (isEmailExist && isUserNameExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל ושם המשתמש שהקלדת כבר קיימים במערכת, בבקשה תבחר אימייל ושם משתמש חדשים ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    else if (isEmailExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל שהקלדת כבר קיים במערכת, בבקשה תבחר אימייל חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    else
                        await App.Current.MainPage.DisplayAlert("שגיאה", "שם המשתמש שהקלדת כבר קיים במערכת, בבקשה תבחר שם משתמש חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }



        //public SignUpViewModel()
        //{
        //    SaveDataCommand = new Command(SaveData);

        //    //AdultSignUpCommand = new Command(OnAdultSignUp);
        //}

        //public async void OnAdultSignUp()
        //{
        //    CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
        //    User user = new User()
        //    {
        //        Email = this.Email,
        //        UserName = this.UserName,
        //        UserPswd = this.Password,
        //        FirstName = this.Name,
        //        LastName = this.LastName,
        //        BirthDate = this.BirthDate,
        //        PhoneNum = this.PhoneNum,
        //        Photo = this.userImgSrc,
        //        City = this.City,
        //        Neighborhood = this.Neighborhood,
        //        Street = this.Street,
        //        HouseNum = this.HouseNum
        //    };
        //    Adult adult = new Adult()
        //    {
        //        IdNavigation = user
        //    };
        //    Adult newAdult = await proxy.AdultSignUpAsync(adult);

        //    //this.Email, this.UserName, this.Password, this.FirstName, this.LastName,
        //    //    this.BirthDate, this.PhoneNumber, this.Photo, this.City, this.Neighborhood, this.Street, this.HouseNumber

        //    if (newAdult != null)
        //    {      
        //        App a = (App)App.Current;
        //        //a.CurrentUser = user;
        //        a.CurrentAdult = newAdult;

        //        AdultPage ap = new AdultPage();
        //        ap.Title = "Adult Page";
        //        //a.MainPage = ap;
        //        await App.Current.MainPage.Navigation.PushAsync(ap);
        //    }
        //    else
        //    {
        //        //await App.Current.MainPage.Navigation.PopModalAsync();
        //        await App.Current.MainPage.DisplayAlert("Error", "Sign Up failed! please try again!", "OK");
        //    }
        //}


        //The following command handle the pick photo button


        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
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

        //The following command handle the take photo button
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
    }
}
