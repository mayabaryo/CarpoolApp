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

namespace CarpoolApp.ViewModels
{
    class AdultSignUpViewModel : INotifyPropertyChanged
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
        private ContactPhone phoneNumber;
        public ContactPhone PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
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
        private int houseNumber;
        public int HouseNumber
        {
            get { return houseNumber; }
            set
            {
                houseNumber = value;
                OnPropertyChanged("HouseNumber");
            }
        }
        #endregion

        //Kid Details
        #region Kid Details
        private string kidUserName;
        public string KidUserName
        {
            get { return kidUserName; }
            set
            {
                kidUserName = value;
                OnPropertyChanged("KidUserName");
            }
        }
        private string kidPassword;
        public string KidPassword
        {
            get { return kidPassword; }
            set
            {
                kidPassword = value;
                OnPropertyChanged("KidPassword");
            }
        }
        private string kidFirstName;
        public string KidFirstName
        {
            get { return kidFirstName; }
            set
            {
                kidFirstName = value;
                OnPropertyChanged("KidFirstName");
            }
        }
        private string kidLasttName;
        public string KidLastName
        {
            get { return kidLasttName; }
            set
            {
                kidLasttName = value;
                OnPropertyChanged("KidLastName");
            }
        }
        private DateTime kidBirthDate;
        public DateTime KidBirthDate
        {
            get { return kidBirthDate; }
            set
            {
                kidBirthDate = value;
                OnPropertyChanged("KidBirthDate");
            }
        }
        private ContactPhone kidPhoneNumber;
        public ContactPhone KidPhoneNumber
        {
            get { return kidPhoneNumber; }
            set
            {
                kidPhoneNumber = value;
                OnPropertyChanged("KidPhoneNumber");
            }
        }
        private string kidCity;
        public string KidCity
        {
            get { return kidCity; }
            set
            {
                kidCity = value;
                OnPropertyChanged("KidCity");
            }
        }
        #endregion
    }
}
