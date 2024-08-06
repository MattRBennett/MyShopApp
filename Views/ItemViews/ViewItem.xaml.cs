using MyShopApp.Models;
using MyShopApp.Services;

namespace MyShopApp.Views.ItemViews;

public partial class ViewItem : ContentPage
{
	public int ItemID;
    Item item = new();

    public ViewItem(int SelectedItemID)
	{
		InitializeComponent();
		
		ItemID = SelectedItemID;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        
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
}