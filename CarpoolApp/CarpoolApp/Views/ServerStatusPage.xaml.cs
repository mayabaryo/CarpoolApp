using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarpoolApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServerStatusPage : ContentPage
    {
        public ServerStatusPage(Object bindingContext)
        {
            this.BindingContext = bindingContext;
            InitializeComponent();
        }
    }
}