using MyShopApp.Models;
using Newtonsoft.Json;

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
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        UsersID = Preferences.Get("UsersID", 0);

        if (UsersID > 0)
        {
            var CartItem = await App.Service.GetCartByUserID(UsersID);

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
                
                CartCollectionView.ItemsSource = CartItemsList;
            }
            else
            {
                await DisplayAlert("Error", "There are no items in the cart to display.", "Ok");
                return;
            }
                
        }
        else
        {
            await DisplayAlert("Error", "User is not logged in, please login to see your cart.", "Ok");
            return;
        }
        LoadingIndicatorStack.IsVisible = false;
        Content.IsVisible = true;

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

            if (UsersID > 0)
            {
                var CartItem = await App.Service.GetCartByUserID(UsersID);

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

                    CartCollectionView.ItemsSource = CartItemsList;
                }
                else
                {
                    await DisplayAlert("Error", "There are no items in the cart to display.", "Ok");
                    return;
                }

            }
            else
            {
                await DisplayAlert("Error", "User is not logged in, please login to see your cart.", "Ok");
                return;
            }
        }
        
    }
}