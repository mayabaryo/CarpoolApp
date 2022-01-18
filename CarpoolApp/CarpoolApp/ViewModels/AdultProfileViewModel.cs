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
    class AdultProfileViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region FirstName
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region LastName
        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        #endregion

        #region Password
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #region UserName
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        #endregion

        #region Email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        #endregion

        #region PhoneNum
        private string phoneNum;
        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                OnPropertyChanged("PhoneNum");
            }
        }
        #endregion

        #region BirthDate
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
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
        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        #endregion

        #region Street
        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
        #endregion

        #region HouseNum
        private int houseNum;
        public int HouseNum
        {
            get => houseNum;
            set
            {
                houseNum = value;
                OnPropertyChanged("HouseNum");
            }
        }
        #endregion

        #region Constructor
        public AdultProfileViewModel()
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
            this.Street = currentUser.Street;
            this.HouseNum = currentUser.HouseNum;

            //set the path url to the contact photo
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            //Create a source with cache busting!
            Random r = new Random();
            this.UserImgSrc = currentUser.PhotoURL + $"?{r.Next()}";
        }
        #endregion

        #region UpdateCommand
        public ICommand UpdateCommand => new Command(OnUpdate);
        public async void OnUpdate()
        {
            Page page = new UpdateUser();
            page.Title = "עדכון פרטים";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region HomeCommand
        public ICommand HomeCommand => new Command(OnHome);
        public async void OnHome()
        {
            App theApp = (App)App.Current;
            Page page = new UpdateUser();
            page.Title = $"שלום {theApp.CurrentUser.UserName}";

            await App.Current.MainPage.Navigation.PopToRootAsync();
            //while (App.Current. != App.Current.MainPage)
            //await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region AddKidCommand
        public ICommand AddKidPageCommand => new Command(OnAddKid);
        public async void OnAddKid()
        {
            AddKid page = new AddKid();
            page.Title = "הוסף ילד";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region LogOutCommand
        public ICommand LogOutCommand => new Command(OnLogOut);
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
