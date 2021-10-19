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
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "This is a required field";
        public const string BAD_EMAIL = "Invalid email";
        public const string SHORT_PASS = "The password must contain at least 6 characters";
        public const string BAD_PHONE = "Invalid phone number (must contain 10 digits)";
    }

    public class SignUpViewModel : INotifyPropertyChanged
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
                if (this.PhoneNum.Length != 10)
                {
                    this.ShowPhoneNumError = true;
                    this.PhoneNumError = ERROR_MESSAGES.BAD_PHONE;
                }
            }
            else
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
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

        private void ValidateBirthDate()
        {
            int nowYear = DateTime.Now.Year;
            int nowMonth = DateTime.Now.Month;
            int nowDay = DateTime.Now.Day;

            this.ShowBirthDateError = BirthDate != null; /*DateTime.IsNullOrEmpty(BirthDate) || DateTime.Now-BirthDate*/
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

        private string houseNum;

        public string HouseNum
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
            this.ShowHouseNumError = string.IsNullOrEmpty(HouseNum);
        }
        #endregion

        //private User theUser;
        private Adult theAdult;

        public SignUpViewModel(Adult a = null)
        {
            //create a new user contact if this is an add operation
            if (a == null)
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
                    BirthDate = default,
                    PhoneNum = "",
                    City = "",
                    Neighborhood = "",
                    Street = "",
                    HouseNum = ""
                };
                a = new Adult()
                {
                    IdNavigation = u
                };

                //a = new User()
                //{
                //    UserId = theApp.CurrentUser.Id,
                //    FirstName = "",
                //    LastName = "",
                //    Email = "",
                //    ContactPhones = new List<Models.ContactPhone>()
                //};

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
                this.UserImgSrc = proxy.GetBasePhotoUri() + a.Id + $".jpg?{r.Next()}";
            }

            this.theAdult = a;
            this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
            this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PasswordError = ERROR_MESSAGES.SHORT_PASS;
            this.NameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.BirthDateError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.NeighborhoodError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.HouseNumError = ERROR_MESSAGES.REQUIRED_FIELD;

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
            this.ShowHouseNumError = false;

            //this.ContactPhones = new ObservableCollection<Models.ContactPhone>(uc.ContactPhones);
            this.SaveDataCommand = new Command(() => SaveData());
            //this.Name = a.FirstName;
            //this.LastName = a.LastName;
            //this.Email = a.Email;

            this.Email = a.IdNavigation.Email;
            this.UserName = a.IdNavigation.UserName;
            this.Password = a.IdNavigation.UserPswd;
            this.Name = a.IdNavigation.FirstName;
            this.LastName = a.IdNavigation.LastName;
            this.BirthDate = a.IdNavigation.BirthDate;
            this.PhoneNum = a.IdNavigation.PhoneNum;
            this.City = a.IdNavigation.City;
            this.Neighborhood = a.IdNavigation.Neighborhood;
            this.Street = a.IdNavigation.Street;
            this.HouseNum = a.IdNavigation.HouseNum;

            //this.theAdult.IdNavigation = this.theUser;
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
            //ValidateBirthDate();
            ValidatePhoneNum();
            ValidateCity();
            ValidateNeighborhood();
            ValidateStreet();
            ValidateHouseNum();

            //check if any validation failed
            if (ShowEmailError || ShowUserNameError || ShowPasswordError || ShowNameError
                || ShowLastNameError /*|| ShowBirthDateError*/ || ShowPhoneNumError|| ShowCityError
                || ShowNeighborhoodError || ShowStreetError || ShowHouseNumError)
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
                this.theAdult.IdNavigation.Email = this.Email;
                this.theAdult.IdNavigation.UserName = this.UserName;
                this.theAdult.IdNavigation.UserPswd = this.Password;
                this.theAdult.IdNavigation.FirstName = this.Name;
                this.theAdult.IdNavigation.LastName = this.LastName;
                this.theAdult.IdNavigation.BirthDate = this.BirthDate;
                this.theAdult.IdNavigation.PhoneNum = this.PhoneNum;
                this.theAdult.IdNavigation.City = this.City;
                this.theAdult.IdNavigation.Neighborhood = this.Neighborhood;
                this.theAdult.IdNavigation.Street = this.Street;
                this.theAdult.IdNavigation.HouseNum = this.HouseNum;

                //this.theAdult.IdNavigation = this.theUser;

                ServerStatus = "Connecting to server...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
                Adult newAdult = await proxy.AdultSignUpAsync(this.theAdult);
                if (newAdult == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Sign Up faild", "OK");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "Uploading photo";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{newAdult.Id}.jpg");
                    }
                    ServerStatus = "Saving data...";
                    //if someone registered to get the contact added event, fire the event
                    if (this.ContactUpdatedEvent != null)
                    {
                        this.ContactUpdatedEvent(newAdult, this.theAdult);
                    }
                    //close the message and add contact windows!

                    await App.Current.MainPage.Navigation.PopAsync();
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Save data", "There is a problen with the data, please try again", "OK");
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
                Title = "Pick a photo"
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
                Title = "Take a photo"
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


        ////Adult Details
        //#region Adult Details
        //private string email;
        //public string Email
        //{
        //    get { return email; }
        //    set
        //    {
        //        email = value;
        //        OnPropertyChanged("Email");
        //    }
        //}
        //private string userName;
        //public string UserName
        //{
        //    get { return userName; }
        //    set
        //    {
        //        userName = value;
        //        OnPropertyChanged("UserName");
        //    }
        //}
        //private string password;
        //public string Password
        //{
        //    get { return password; }
        //    set
        //    {
        //        password = value;
        //        OnPropertyChanged("Password");
        //    }
        //}
        //private string firstName;
        //public string FirstName
        //{
        //    get { return firstName; }
        //    set
        //    {
        //        firstName = value;
        //        OnPropertyChanged("FirstName");
        //    }
        //}
        //private string lasttName;
        //public string LastName
        //{
        //    get { return lasttName; }
        //    set
        //    {
        //        lasttName = value;
        //        OnPropertyChanged("LastName");
        //    }
        //}
        //private DateTime birthDate;
        //public DateTime BirthDate
        //{
        //    get { return birthDate; }
        //    set
        //    {
        //        birthDate = value;
        //        OnPropertyChanged("BirthDate");
        //    }
        //}
        //private string phoneNumber;
        //public string PhoneNumber
        //{
        //    get { return phoneNumber; }
        //    set
        //    {
        //        phoneNumber = value;
        //        OnPropertyChanged("PhoneNumber");
        //    }
        //}
        //private string photo;
        //public string Photo
        //{
        //    get 
        //    {
        //        if (string.IsNullOrEmpty(this.photo))
        //            return DEFAULT_PHOTO_SRC;
        //        return photo; 
        //    }
        //    set
        //    {
        //        photo = value;
        //        OnPropertyChanged("Photo");
        //    }
        //}
        //private const string DEFAULT_PHOTO_SRC = "defaultphoto.jpg";

        //private string city;
        //public string City
        //{
        //    get { return city; }
        //    set
        //    {
        //        city = value;
        //        OnPropertyChanged("City");
        //    }
        //}
        //private string neighborhood;
        //public string Neighborhood
        //{
        //    get { return neighborhood; }
        //    set
        //    {
        //        neighborhood = value;
        //        OnPropertyChanged("Neighborhood");
        //    }
        //}
        //private string street;
        //public string Street
        //{
        //    get { return street; }
        //    set
        //    {
        //        street = value;
        //        OnPropertyChanged("Street");
        //    }
        //}
        //private string houseNumber;
        //public string HouseNumber
        //{
        //    get { return houseNumber; }
        //    set
        //    {
        //        houseNumber = value;
        //        OnPropertyChanged("HouseNumber");
        //    }
        //}
        //#endregion

        //public ICommand AdultSignUpCommand { protected set; get; }
    }
}
