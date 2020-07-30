using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void TapBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void BtnCall_OnClicked(object sender, EventArgs e)
        {
            PhoneDialer.Open("0123456789");
        }
    }
}