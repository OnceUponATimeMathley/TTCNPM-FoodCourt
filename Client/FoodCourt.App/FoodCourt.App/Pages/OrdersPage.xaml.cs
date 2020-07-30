using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodCourt.App.Models;
using FoodCourt.App.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public ObservableCollection<OrderByUser> OrdersCollection;
        public OrdersPage()
        {
            InitializeComponent();
            OrdersCollection = new ObservableCollection<OrderByUser>();
            GetOrderItems();
        }

        private async void GetOrderItems()
        {
            var orders = await APIService.GetOrdersByUser(Preferences.Get("userId", 0));
            foreach (var order in orders)
            {
                OrdersCollection.Add(order);
            }
            LvOrders.ItemsSource = OrdersCollection;
        }

        private void TapBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}