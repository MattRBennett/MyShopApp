using MyShopApp.Models;

namespace MyShopApp.Views.ItemViews;

public partial class AddNewItem : ContentPage
{
    List<ItemCategory> categories = new();
    Item item = new Item();
    public AddNewItem()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var FetchedCategories = await App.Service.GetItemCategories();
        categories.AddRange(FetchedCategories);
        CategoryOfItem.ItemsSource = categories;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        var selectedCategory = (ItemCategory)CategoryOfItem.SelectedItem;

        Item NewItemDetails = new Item
        {
            Name = NameOfItem.Text,
            Description = DescriptionOfItem.Text,
            Price = decimal.Parse(PriceOfItem.Text),
            ItemsCategory = selectedCategory
        };

        await item.AddNewItem(NewItemDetails);
        await Navigation.PopModalAsync();
    }
}