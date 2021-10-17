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
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            SignUpViewModel vm = new SignUpViewModel();
            this.BindingContext = vm;
            vm.SetImageSourceEvent += OnSetImageSource;
            InitializeComponent();
        }

        //public SignUp(SignUpViewModel context)
        //{
        //    this.BindingContext = context;
        //    context.SetImageSourceEvent += OnSetImageSource;
        //    InitializeComponent();
        //}

        public void OnSetImageSource(ImageSource imgSource)
        {
            theImage.Source = imgSource;
        }
    }
}