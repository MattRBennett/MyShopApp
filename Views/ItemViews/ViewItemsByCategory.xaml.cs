using MyShopApp.Models;

namespace MyShopApp.Views.ItemViews;

public partial class ViewItemsByCategory : ContentPage
{
	public CategoryList ItemsCategory;

	public List<Item> items = new();
	public ViewItemsByCategory(CategoryList ItemCategory)
	{
		InitializeComponent();

        ItemsCategory = ItemCategory;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        Content.IsVisible = false;
        NothingFound.IsVisible = false;

        CategoryNameLabel.Text = ItemsCategory.CategoryName;

        ItemCategory category = (ItemCategory)ItemsCategory.CategoryID;

        var GetItems = await App.Service.GetItemsByCategory(category);
        if (GetItems.Count > 0)
        {
            items.AddRange(GetItems);
            ItemsCollectionView.ItemsSource = items;
            Content.IsVisible = true;
        }
		else
        {
            NothingFound.IsVisible = true;
        }
    }

    private async void ItemFrame_Tapped(object sender, TappedEventArgs e)
    {
		if (sender is Frame frame && frame.BindingContext is Item item) 
		{
			await Navigation.PushModalAsync(new ViewItem(item.Id));
		}
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}