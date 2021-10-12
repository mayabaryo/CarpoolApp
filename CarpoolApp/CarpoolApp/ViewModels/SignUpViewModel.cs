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

namespace CarpoolApp.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //Adult Details
        #region Adult Details
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string lasttName;
        public string LastName
        {
            get { return lasttName; }
            set
            {
                lasttName = value;
                OnPropertyChanged("LastName");
            }
        }
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        private string photo;
        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        private string neighborhood;
        public string Neighborhood
        {
            get { return neighborhood; }
            set
            {
                neighborhood = value;
                OnPropertyChanged("Neighborhood");
            }
        }
        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
        private string houseNumber;
        public string HouseNumber
        {
            get { return houseNumber; }
            set
            {
                houseNumber = value;
                OnPropertyChanged("HouseNumber");
            }
        }
        #endregion

        public ICommand AdultSignUpCommand { protected set; get; }
        public SignUpViewModel()
        {
            AdultSignUpCommand = new Command(OnAdultSignUp);
        }
        public async void OnAdultSignUp()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            User user = new User()
            {
                Email = this.Email,
                UserName = this.UserName,
                UserPswd = this.Password,
                FirstName = this.FirstName,
                LastName = this.LastName,
                BirthDate = this.BirthDate,
                PhoneNum = this.PhoneNumber,
                Photo = this.Photo,
                City = this.City,
                Neighborhood = this.Neighborhood,
                Street = this.Street,
                HouseNum = this.HouseNumber
            };
            Adult adult1 = new Adult()
            {
                IdNavigation = user
            };
            Adult adult = await proxy.AdultSignUpAsync(adult1);

            //this.Email, this.UserName, this.Password, this.FirstName, this.LastName,
            //    this.BirthDate, this.PhoneNumber, this.Photo, this.City, this.Neighborhood, this.Street, this.HouseNumber
            
            if (adult != null)
            {      
                App a = (App)App.Current;
                //a.CurrentUser = user;
                a.CurrentAdult = adult;

                AdultPage ap = new AdultPage();
                ap.Title = "Adult Page";
                //a.MainPage = ap;
                await App.Current.MainPage.Navigation.PushAsync(ap);
            }
            else
            {
                //await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("Error", "Sign Up failed! please try again!", "OK");
            }
        }
    }
}
