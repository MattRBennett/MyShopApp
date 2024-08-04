using MyShopApp.Models;
using MyShopApp.Views.ItemViews;

namespace MyShopApp.Views;

public partial class Landing : ContentPage
{
    List<Item> items = new();
    List<ItemCategory> categories = new();
    public Landing()
    {
        InitializeComponent();
        //Content.IsVisible = false;
        //LoadingIndicatorStack.IsVisible = true;
        SearchResultsListView.IsVisible = false;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        ItemSearchBar.Text = "";
        Item item = new();
        items = await item.GetItems();
        var fetchedcategories = await App.Service.GetItemCategories();

        categories.AddRange(fetchedcategories);
        CategoriesCollectionView.ItemsSource = categories;

        //var myitems = await App.Service.GetItemsByCategory(ItemCategory.Unassigned);

        //LoadingIndicatorStack.IsVisible = false;
        //Content.IsVisible = true;
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
        if (sender is Frame frame && frame.BindingContext is Item ThisItem)
        {
            await Navigation.PushModalAsync(new ViewItem(ThisItem.Id));
        }
    }

    private async void CategoryFrame_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is ItemCategory ThisCategory) 
        {
            await Navigation.PushModalAsync(new ViewItemsByCategory(ThisCategory));
        }
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushModalAsync(new itemde)
    }

    private async void AddNewItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddNewItem());
    }
}