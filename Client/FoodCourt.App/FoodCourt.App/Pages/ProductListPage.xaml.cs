using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodCourt.App.Models;
using FoodCourt.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodCourt.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        public ObservableCollection<ProductByCategory> ProductByCategoryCollection;
        public ProductListPage(int categoryId, string categoryName)
        {
            InitializeComponent();
            LblCategoryName.Text = categoryName;
            ProductByCategoryCollection = new ObservableCollection<ProductByCategory>();
            GetProducts(categoryId);
        }

        private async void GetProducts(int id)
        {
            var products = await APIService.GetProductByCategory(id);
            foreach (var product in products)
            {
                ProductByCategoryCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductByCategoryCollection;
        }

        private void TapBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void CvProducts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currectSelection = e.CurrentSelection.FirstOrDefault() as ProductByCategory;
            if (currectSelection == null) return;
            Navigation.PushModalAsync(new ProductDetailPage(currectSelection.id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}