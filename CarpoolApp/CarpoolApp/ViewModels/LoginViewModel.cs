﻿using System;
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
    class LoginViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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

        public ICommand LoginCommand { protected set; get; }
        public ICommand TapCommand { protected set; get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            TapCommand = new Command(OnTap);
        }

        //private string serverStatus;
        //public string ServerStatus
        //{
        //    get { return serverStatus; }
        //    set
        //    {
        //        serverStatus = value;
        //        OnPropertyChanged("ServerStatus");
        //    }
        //}

        public async void OnLogin()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            User user = await proxy.LoginAsync(this.Email,/* this.UserName,*/ this.Password);

            if (user != null)
            {
                App a = (App)App.Current;
                a.CurrentUser = user;

                if (true)
                {
                    AdultPage ap = new AdultPage();
                    ap.Title = "Adult Page";
                    //a.MainPage = ap;
                    await App.Current.MainPage.Navigation.PushAsync(ap);
                }
                else
                {
                    KidPage kp = new KidPage();
                    kp.Title = "Kid Page";
                    //a.MainPage = kp;
                    await App.Current.MainPage.Navigation.PushAsync(kp);
                }
            }
            else
            {
                //await App.Current.MainPage.Navigation.PopModalAsync();
                await App.Current.MainPage.DisplayAlert("Error", "Login failed! please try another email or password!", "OK");
            }
        }
        public async void OnTap()
        {
            //App theApp = (App)App.Current;
            //SignUpViewModel vm = new SignUpViewModel();
            //vm.ContactUpdatedEvent += OnContactAdded;
            //Page p = new Views.AddContact(vm);
            //await theApp.MainPage.Navigation.PushAsync(p);

            App a = (App)App.Current;
            SignUp su = new SignUp();
            su.Title = "Sign Up";
            await App.Current.MainPage.Navigation.PushAsync(su);
        }



        //public async void OnSubmit()
        //{
        //    ServerStatus = "מתחבר לשרת...";
        //    await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
        //    CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
        //    User user = await proxy.LoginAsync(Email, Password);
        //    if (user == null)
        //    {
        //        await App.Current.MainPage.Navigation.PopModalAsync();
        //        await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, בדוק שם משתמש וסיסמה ונסה שוב", "בסדר");
        //    }
        //    else
        //    {
        //        ServerStatus = "קורא נתונים...";
        //        App theApp = (App)App.Current;
        //        theApp.CurrentUser = user;
        //        bool success = await LoadPhoneTypes(theApp);
        //        if (!success)
        //        {
        //            await App.Current.MainPage.Navigation.PopModalAsync();
        //            await App.Current.MainPage.DisplayAlert("שגיאה", "קריאת נתונים נכשלה. נסה שוב מאוחר יותר", "בסדר");
        //        }
        //        else
        //        {
        //            //Initiate all phone types refrence to the same objects of PhoneTypes
        //            foreach (UserContact uc in user.UserContacts)
        //            {
        //                foreach (Models.ContactPhone cp in uc.ContactPhones)
        //                    cp.PhoneType = theApp.PhoneTypes.Where(pt => pt.TypeId == cp.PhoneTypeId).FirstOrDefault();
        //            }

        //            Page p = new NavigationPage(new Views.ContactsList());
        //            App.Current.MainPage = p;
        //        }


        //    }
        //}

        public ICommand AdultSignUpPage => new Command(ASignUp);
        public async void ASignUp()
        {
            App a = (App)App.Current;
            AdultSignUp page = new AdultSignUp();
            page.Title = "Adult Sign Up";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        public ICommand ManagerSignUpPage => new Command(MSignUp);
        public async void MSignUp()
        {
            App a = (App)App.Current;
            ManagerSignUp page = new ManagerSignUp();
            page.Title = "Manager Sign Up";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }




        public ICommand GetString => new Command(GetS);
        public async void GetS()
        {
            CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
            this.Email = await proxy.GetStringAsync();
        }
    }
}
