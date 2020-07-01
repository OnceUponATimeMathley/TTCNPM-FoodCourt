using System;
using FoodCourt.App.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignupPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
