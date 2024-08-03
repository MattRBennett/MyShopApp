using MyShopApp.Models;
using MyShopApp.Views.ItemViews;

namespace MyShopApp.Views;

public partial class Landing : ContentPage
{
    List<Item> items = new();
    public Landing()
	{
		InitializeComponent();
        Content.IsVisible = false;
        LoadingIndicatorStack.IsVisible = true;
        SearchResultsListView.IsVisible = false;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        
        Item item = new();
        items = await item.GetItems();
        LoadingIndicatorStack.IsVisible = false;
        Content.IsVisible = true;

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //var route = $"{nameof(ViewAllItems)}";

        //await Shell.Current.GoToAsync(route);
        await Navigation.PushModalAsync(new ViewAllItems());
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("..");
        await Navigation.PopModalAsync();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (ItemSearchBar == null || SearchResultsListView == null || items == null)
        {
            return;
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(ItemSearchBar.Text))
            {
                string searchText = ItemSearchBar.Text.ToLower();
                SearchResultsListView.IsVisible = true;
                var filteredItems = items.Where(item =>
                item.Name.ToLower().Contains(searchText));

                if (filteredItems.Any())
                {
                    SearchResultsListView.ItemsSource = filteredItems;
                }
                else
                {
                    SearchResultsListView.IsVisible = false;
                }


                }
            else
            {
                SearchResultsListView.IsVisible = false;
                SearchResultsListView.ItemsSource = items;
                return;
            }
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Item thisItem)
        {
            await Navigation.PushModalAsync(new ViewItem(thisItem.Id));
        }
    }
}