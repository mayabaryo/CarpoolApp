using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarpoolApp.ViewModels;

namespace CarpoolApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            this.BindingContext = new NotificationsViewModel();
            InitializeComponent();
        }
    }
}