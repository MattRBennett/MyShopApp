using MyShopApp.Models;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace MyShopApp.Views.CartViews;

public partial class ViewCart : ContentPage
{
    List<PresentableItems> CartItemsList = new();
    public int UsersID;

    public class PresentableItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public ImageSource Image { get; set; }
    }
    public ViewCart()
    {
        InitializeComponent();
        LoadingIndicatorStack.IsVisible = true;
        Content.IsVisible = false;
        NothingFound.IsVisible = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        UsersID = Preferences.Get("UsersID", 0);

        LoadCart();
        CheckIfCartHasItems();

        LoadingIndicatorStack.IsVisible = false;
        Content.IsVisible = true;
    }

    public async void LoadCart()
    {
        if (UsersID > 0)
        {
            var CartItem = await App.Service.GetCartByUserID(UsersID);
            if (CartItem.Data is null)
                return;

            if (!string.IsNullOrWhiteSpace(CartItem.Data.CartItems))
            {
                var DeserializeItems = JsonConvert.DeserializeObject<List<Item>>(CartItem.Data.CartItems);

                foreach (var item in DeserializeItems)
                {
                    var imageSource = item.Image != null && item.Image.Length > 0
                ? ImageSource.FromStream(() => new MemoryStream(item.Image))
                : null;

                    CartItemsList.Add(new PresentableItems
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = $"£{item.Price}",
                        Image = imageSource
                    });
                }

                CheckIfCartHasItems();
            }
            else
            {
                NothingFound.IsVisible = true;
                PurchaseButton.IsEnabled = false;
                return;
            }
        }
        else
        {
            await DisplayAlert("Error", "User is not logged in, please login to see your cart.", "Ok");
            return;
        }
    }

    public void CheckIfCartHasItems()
    {
        if (CartItemsList.Count > 0)
        {
            PurchaseButton.IsEnabled = true;
            NothingFound.IsVisible = false;
            CartCollectionView.ItemsSource = CartItemsList;
        }
        else
        {
            PurchaseButton.IsEnabled = false;
            NothingFound.IsVisible = true;
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void SwipeItemView_Invoked(object sender, EventArgs e)
    {
        if (sender is SwipeItemView SIV && SIV.BindingContext is PresentableItems presentableItems)
        {
            await App.Service.RemoveCartItem(UsersID, presentableItems.Id);

            CartCollectionView.ItemsSource = null;
            CartItemsList.Clear();

            LoadCart();
        }
        else
        {
            Console.WriteLine("Nothing was selected.");
        }

    }

    private async void PurchaseButton_Clicked(object sender, EventArgs e)
    {
        if (CartItemsList.Count > 0)
        {
            await App.Service.RemoveCart(UsersID);
            await DisplayAlert("Cart", "Your items have successully been purchased!", "Ok");
            LoadCart();
            CheckIfCartHasItems();
        }
        else
        {
            await DisplayAlert("Error", "There are no items in your cart to purchase.", "Ok");
        }
    }
}