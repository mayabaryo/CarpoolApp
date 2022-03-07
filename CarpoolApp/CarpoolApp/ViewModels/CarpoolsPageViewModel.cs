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
    class CarpoolsPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Kid Kid { get; set; }
        public ObservableCollection<Carpool> CarpoolList { get; set; }
        public Activity Activity { get; set; }
        public ObservableCollection<Carpool> OtherCarpools { get; set; }
        public Carpool MyCarpool { get; set; }
        public bool ShowCarpool { get; set; }
        public bool ShowLabel { get => !ShowCarpool; }


        public CarpoolsPageViewModel()
        {

        }

        #region AddCarpoolCommand
        public ICommand RequestToJoinCommand => new Command<Carpool>(OnRequestToJoin);
        public async void OnRequestToJoin(Carpool carpool)
        {
            RequestToJoinCarpool requestToJoinCarpool = new RequestToJoinCarpool()
            {
                KidId = Kid.Id,
                CarpoolId = carpool.Id,
                RequestStatusId = 1
            };

            //Page page = new AddCarpool();

            //AddCarpoolViewModel carpoolContext = new AddCarpoolViewModel()
            //{
            //    Activity = activity,
            //    Kid = this.Kid
            //};
            //page.BindingContext = carpoolContext;
            //page.Title = "צור הסעה";
            //await App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion
    }
}
