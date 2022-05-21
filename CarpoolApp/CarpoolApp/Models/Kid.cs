using CarpoolApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CarpoolApp.Models
{
    public partial class Kid : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Kid()
        {
            KidsInActivities = new HashSet<KidsInActivity>();
            KidsInCarpools = new HashSet<KidsInCarpool>();
            KidsOfAdults = new HashSet<KidsOfAdult>();
        }

        public int Id { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<KidsInActivity> KidsInActivities { get; set; }
        public virtual ICollection<KidsInCarpool> KidsInCarpools { get; set; }
        public virtual ICollection<KidsOfAdult> KidsOfAdults { get; set; }

        //Added only to client side
        //public bool IsInCarpool { get; set; }

        #region IsInCarpool
        private bool isInCarpool;
        public bool IsInCarpool
        {
            get => isInCarpool;
            set
            {
                isInCarpool = value;
                OnPropertyChanged("IsInCarpool");
            }
        }
        #endregion
    }
}
