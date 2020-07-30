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
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<PopularProduct> ProductsCollection;
        public ObservableCollection<Category> CategoriesCollection;
        public HomePage()
        {
            InitializeComponent();
            ProductsCollection = new ObservableCollection<PopularProduct>();
            CategoriesCollection = new ObservableCollection<Category>();
            GetPopularProducts();
            GetCategories();
            LblUserName.Text = Preferences.Get("userName", string.Empty);
        }

        private async void GetCategories()
        {
            var categories = await APIService.GetCategories();
            foreach (var category in categories)
            {
                CategoriesCollection.Add(category);
            }
            CvCategories.ItemsSource = CategoriesCollection;
        }

        private async void GetPopularProducts()
        {
            var products = await APIService.GetPopularProducts();
            foreach (var product in products)
            {
                ProductsCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductsCollection;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CloseAppMenu();
        }

        private async void CloseAppMenu()
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var response = await APIService.GetTotalCartItems(Preferences.Get("userId", 0));
            LblTotalItems.Text = response.totalItems.ToString();
        }

        private void CvProducts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currectSelection = e.CurrentSelection.FirstOrDefault() as PopularProduct;
            if (currectSelection == null) return;
            Navigation.PushModalAsync(new ProductDetailPage(currectSelection.id));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void CvCategories_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new ProductListPage(currentSelection.id, currentSelection.name));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void TapCartIcon_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CartPage());
        }

        private void TapOrders_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new OrdersPage());
        }

        private async void ImgMenu_OnTapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private void TapCloseMenu_OnTapped(object sender, EventArgs e)
        {
            CloseAppMenu();
        }

        private void TapContact_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ContactPage());
        }

        private void TapCart_OnTapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CartPage());
        }
    }
}