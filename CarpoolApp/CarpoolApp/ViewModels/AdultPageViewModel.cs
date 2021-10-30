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
    class AdultPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ICommand HomePageCommand { protected set; get; }
        public ICommand AddKidPageCommand { protected set; get; }
        public AdultPageViewModel()
        {
            HomePageCommand = new Command(OnHome);
            AddKidPageCommand = new Command(OnAddKid);
        }

        public async void OnHome()
        {
            App theApp = (App)App.Current;
            AdultPage page = new AdultPage();
            page.Title = $"שלום {theApp.CurrentUser.UserName}";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
        public async void OnAddKid()
        {
            AddKid page = new AddKid();
            page.Title = "הוסף ילד";
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
