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
    class AddCarpoolViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region CarpoolTime
        private bool showCarpoolTimeError;
        public bool ShowCarpoolTimeError
        {
            get => showCarpoolTimeError;
            set
            {
                showCarpoolTimeError = value;
                OnPropertyChanged("ShowCarpoolTimeError");
            }
        }

        private DateTime carpoolTime;
        public DateTime CarpoolTime
        {
            get => carpoolTime;
            set
            {
                carpoolTime = value;
                ValidateCarpoolTime();
                OnPropertyChanged("CarpoolTime");
            }
        }

        private string carpoolTimeError;
        public string CarpoolTimeError
        {
            get => carpoolTimeError;
            set
            {
                carpoolTimeError = value;
                OnPropertyChanged("CarpoolTimeError");
            }
        }

        private void ValidateCarpoolTime()
        {
            TimeSpan ts = this.CarpoolTime - DateTime.Now;
            this.ShowCarpoolTimeError = ts.TotalMinutes < 0;
        }
        #endregion        
    }
}
