using MyShopApp.Models;
using MyShopApp.Services;
using Newtonsoft.Json;
using System.Text.Json;

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
        var GetCartItems = await App.Service.GetCartByUserID(UsersID);

        Item items = new Item
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            Image = item.Image,
            ItemsCategory = item.ItemsCategory
        };

        if (GetCartItems.Data != null) 
        {
            List<Item> ItemList = new();
            ItemList.Add(items);
            var CurrentItems = JsonConvert.DeserializeObject<List<Item>>(GetCartItems.Data.CartItems);
           foreach (var item in CurrentItems)
            {
                ItemList.Add(item);
            }
            
            var FinalSerialization = JsonConvert.SerializeObject(ItemList);

            Cart cart = new Cart
            {
                UserID = UsersID,
                CartItems = FinalSerialization
            };

            await App.Service.AddNewCart(cart);
            await DisplayAlert("Cart", "Item has been added to your cart!", "Ok");
            await Navigation.PopModalAsync();
            return;
        }
        else 
        {
            List<Item> ItemsList = new List<Item>();
            ItemsList.Add(items);

            string SerializedItems = System.Text.Json.JsonSerializer.Serialize(ItemsList);
            Cart cart = new Cart
            {
                UserID = UsersID,
                CartItems = SerializedItems
            };
            await App.Service.AddNewCart(cart);
            await DisplayAlert("Cart", "Item has been added to your cart!", "Ok");
            await Navigation.PopModalAsync();
            return;
        }

        
    }
}