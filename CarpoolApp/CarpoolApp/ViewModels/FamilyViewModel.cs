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
