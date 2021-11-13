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
    public partial class AddKid : ContentPage
    {
        public AddKid()
        {
            AddKidViewModel vm = new AddKidViewModel();
            this.BindingContext = vm;
            vm.SetImageSourceEvent += OnSetImageSource;
            InitializeComponent();
        }

        public void OnSetImageSource(ImageSource imgSource)
        {
            theImage.Source = imgSource;
        }
    }
}