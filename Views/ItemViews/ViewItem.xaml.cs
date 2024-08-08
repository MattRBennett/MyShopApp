using MyShopApp.Models;
using MyShopApp.Services;

namespace MyShopApp.Views.ItemViews;

public partial class ViewItem : ContentPage
{
	public int ItemID;
    Item item = new();
    public int UsersID = 0;

    public ViewItem(int SelectedItemID)
	{
		InitializeComponent();
		
		ItemID = SelectedItemID;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        UsersID = Preferences.Get("UsersID", 0);


        item = await item.GetItemByItemID(ItemID);
        if (item != null)
        {
            ItemName.Text = item.Name;
            ItemPrice.Text = $"£{item.Price.ToString()}";
            ItemCategory.Text = item.ItemsCategory.ToString();
            ItemDescription.Text = item.Description;
            ItemImage.Source = item.Image != null && item.Image.Length > 0
            ? ImageSource.FromStream(() => new MemoryStream(item.Image))
            : null;
        }
        else
        {
            await DisplayAlert("Error", "There was an error obtaining item information!", "Ok");
        }

        string username = Preferences.Get("Username", string.Empty);

        if (!string.IsNullOrEmpty(username))
        {
            AdminControls.IsVisible = true;
        }
        else
        {
            AdminControls.IsVisible = false;
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();

        //await Shell.Current.GoToAsync("..");
    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new EditItem(ItemID));
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Warning", "Are you sure you want to delete this item?", "Yes", "Cancel");
    }

    private async void AddItemToCart_Clicked(object sender, EventArgs e)
    {
        //if (item != null)
        //{
        //    List<Item> newItems = new();
        //    newItems.Add(item);

        //    Cart cart = new Cart 
        //    { 
        //        UserID = UsersID,
        //        CartItems = newItems,
        //        CartTotal = item.Price

        //    };


        //    await App.Service.AddCart(cart);
        //}

        var UserCart = await App.Service.GetCartByUserID(UsersID);

        if (UserCart.Data == null)
        {

            Cart cart = new Cart
            {
                UserID = UsersID,
                CartTotal = 0
            };

            await App.Service.AddNewCart(cart);
        }
    }
}