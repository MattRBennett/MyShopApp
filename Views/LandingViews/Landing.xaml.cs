using MyShopApp.Models;
using MyShopApp.Views.AuthenticationViews;
using MyShopApp.Views.CartViews;
using MyShopApp.Views.ItemViews;

namespace MyShopApp.Views;

public partial class Landing : ContentPage
{
    List<Item> items = new();
    List<CategoryList> categories = new();
    public int UsersID = 0;
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

        ItemSearchBar.Text = "";
        Item item = new();
        items = await item.GetItems();
        //var fetchedcategories = await App.Service.GetItemCategories();
        List<CategoryList> fetchedcategories = await item.GetAllCategoryItems();
        categories.AddRange(fetchedcategories);
        CategoriesCollectionView.ItemsSource = categories;

        UsersID = Preferences.Get("UsersID", 0);

        if (UsersID > 0)
        {
            //logout
            LogInOutButton.Source = ImageSource.FromFile("logout.png");
            SignupBanner.IsVisible = false;
        }
        else if (UsersID == 0)
        {
            //login
            LogInOutButton.Source = ImageSource.FromFile("login.png");
            SignupBanner.IsVisible = true;

        }

        //var myitems = await App.Service.GetItemsByCategory(ItemCategory.Unassigned);

        LoadingIndicatorStack.IsVisible = false;
        Content.IsVisible = true;
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //var route = $"{nameof(ViewAllItems)}";

        //await Shell.Current.GoToAsync(route);
        await Navigation.PushModalAsync(new ViewAllItems());
    }

    private async void EditItem_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("..");
        //await Navigation.PopModalAsync();


        //await Shell.Current.GoToAsync("//MainPage");

        
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

            //var route = $"{nameof(ViewItem)}";

            //await Shell.Current.GoToAsync($"{route}?SelectedItemID={ThisItem.Id}");
        }
    }

    private async void CategoryFrame_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is CategoryList ThisCategory) 
        {
            await Navigation.PushModalAsync(new ViewItemsByCategory(ThisCategory));
            //var route = $"{nameof(ViewItemsByCategory)}";

            //await Shell.Current.GoToAsync($"{route}?ItemCategory={ThisCategory}");
        }
    }

    private async void AddNewItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddNewItem());

        //var route = $"{nameof(AddNewItem)}";

        //await Shell.Current.GoToAsync($"{route}");
    }

    private async void ViewAllCategories_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ViewAllCategories());
    }

    private async void CartButton_Clicked(object sender, EventArgs e)
    {
        if (UsersID > 0)
        {
            await Navigation.PushModalAsync(new ViewCart());
        }
        else
        {
            await DisplayAlert("Cart", "There is nothing in your cart to display, please add items first.", "Ok");
            return;
        }
    }

    private async void LogInOutButton_Clicked(object sender, EventArgs e)
    {
        if (UsersID > 0)
        {
            //logout

            bool LogoutConfirmation = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "Cancel");

            if (LogoutConfirmation)
            {
                Preferences.Clear();
                await Navigation.PushModalAsync(new LoginSignup());
            }

            return;

        }
        else if (UsersID == 0)
        {
            //login
            await Navigation.PushModalAsync(new LoginSignup());

        }
        else
        {
            return;
        }
    }

    private async void NotSignedupButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginSignup());
    }
}