using MyShopApp.Models;

namespace MyShopApp.Views.ItemViews;

public partial class ViewAllCategories : ContentPage
{
    List<Item> items = new();
    List<CategoryList> categories = new();
    public ViewAllCategories()
	{
		InitializeComponent();
	}

	protected async override void OnAppearing()
	{
        Item item = new();
        List<CategoryList> fetchedcategories = await item.GetAllCategoryItems();
        categories.AddRange(fetchedcategories);
        CategoriesCollectionView.ItemsSource = categories;
    }

    private async void Category_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is CategoryList category)
        {
            await Navigation.PushModalAsync(new ViewItemsByCategory(category));
        }
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}