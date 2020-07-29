using System;
using System.Collections.Generic;
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
    public partial class ProductDetailPage : ContentPage
    {
        private int _productId;
        public ProductDetailPage(int productId)
        {
            InitializeComponent();
            GetProductDetails(productId);
            _productId = productId;
        }

        private async void GetProductDetails(int productId)
        {
            var product = await APIService.GetProductById(productId);
            LblName.Text = product.name;
            LblDetail.Text = product.detail;
            ImgProduct.Source = product.FullImageUrl;
            LblPrice.Text = product.price.ToString();
            LblTotalPrice.Text = LblPrice.Text;
        }

        private void TapDecrement_OnTapped(object sender, EventArgs e)
        {
            var i = Convert.ToInt16(LblQty.Text);
            i--;
            if (i < 1)
            {
                return;
            }
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text)).ToString();
        }

        private void TapIncrement_OnTapped(object sender, EventArgs e)
        {
            var i = Convert.ToInt16(LblQty.Text);
            i++;
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text)).ToString();
        }

        private void TapBack_OnTapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void BtnAddToCart_OnClicked(object sender, EventArgs e)
        {
            var addToCart = new AddToCart();
            addToCart.Qty = LblQty.Text;
            addToCart.Price = LblPrice.Text;
            addToCart.TotalAmount = LblTotalPrice.Text;
            addToCart.ProductId = _productId;
            addToCart.CustomerId = Preferences.Get("userId", 0);

            var response = await APIService.AddItemsInCart(addToCart);
            if (response)
            {
                await DisplayAlert("", "Your items has been added to the cart", "Alright");
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }
    }
}