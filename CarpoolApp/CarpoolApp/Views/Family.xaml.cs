﻿using System;
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
    public partial class Family : ContentPage
    {
        public Family()
        {
            this.BindingContext = new FamilyViewModel();
            InitializeComponent();
        }

        //public void OnSetImageSource(ImageSource imgSource)
        //{
        //    swipeView
        //}
    }
}