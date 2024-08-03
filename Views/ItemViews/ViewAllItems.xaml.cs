using MyShopApp.Models;
using System.Collections.ObjectModel;

namespace MyShopApp.Views.ItemViews;

public partial class ViewAllItems : ContentPage
{
	List<Item> items = new();
	public ViewAllItems()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
	{
		base.OnAppearing();

        Item item = new();
		items = await item.GetItems();
		ItemsCollecitonView.ItemsSource = items;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("..");
		await Navigation.PopModalAsync();

    }
}