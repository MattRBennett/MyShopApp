using MyShopApp.Models;
using MyShopApp.Services;

namespace MyShopApp.Views.ItemViews;

public partial class ViewItem : ContentPage
{
	public int ItemID;
   
    public ViewItem(int SelectedItemID)
	{
		InitializeComponent();
		
		ItemID = SelectedItemID;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        Item item = new();
        item = await item.GetItemItemID(ItemID);
        if (item != null)
        {
            ItemName.Text = item.Name;
            ItemPrice.Text = item.Price.ToString();
            ItemCategory.Text = item.ItemsCategory.ToString();
            ItemDescription.Text = item.Description;

        }
        else
        {
            await DisplayAlert("Error", "There was an error obtaining item information!", "Ok");
        }
    }
}