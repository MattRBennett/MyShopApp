using MyShopApp.Models;

namespace MyShopApp.Views.CartViews;

public partial class ViewCart : ContentPage
{
    List<Item> CartItemsList = new();
	public ViewCart()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        int UserID = Preferences.Get("UsersID", 0);

        if (UserID > 0)
        {
            var CartItems = await App.Service.GetCartByUserID(UserID);

            if (CartItems.Data != null)
            {

                foreach (var CartItem in CartItems.Data.CartItems)
                {
                    CartItemsList.Add(new Item
                    {
                        Id = CartItem.Id,
                        Name = CartItem.Name,
                        Description = CartItem.Description,
                        Price = CartItem.Price,
                        Image = CartItem.Image,
                        ItemsCategory = CartItem.ItemsCategory
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