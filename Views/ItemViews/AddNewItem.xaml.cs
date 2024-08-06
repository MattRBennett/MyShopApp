using MyShopApp.Models;

namespace MyShopApp.Views.ItemViews;

public partial class AddNewItem : ContentPage
{
    List<ItemCategory> categories = new();
    Item item = new Item();
    public byte[] ImageByteArray;
    public AddNewItem()
    {
        InitializeComponent();
        ImageStack.IsVisible = false;
        
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var FetchedCategories = await App.Service.GetItemCategories();
        categories.AddRange(FetchedCategories);
        CategoryOfItem.ItemsSource = categories;
        CategoryOfItem.SelectedIndex = 0;
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
            Image = ImageByteArray,
            ItemsCategory = selectedCategory
        };

        await item.AddNewItem(NewItemDetails);
        await Navigation.PopModalAsync();
    }


    private async void AddImageButton_Clicked(object sender, EventArgs e)
    {
        ImageByteArray = await item.AddImage();

        ChosenImage.Source = ImageByteArray != null && ImageByteArray.Length > 0
    ? ImageSource.FromStream(() => new MemoryStream(ImageByteArray))
    : null;

        if (ImageByteArray.Length > 0)
        {
            ImageStack.IsVisible = true;
            AddImageButton.IsVisible = false;
        }
        else
        {
            ImageStack.IsVisible = false;
            AddImageButton.IsVisible = true;
        }
    }



    private void RemoveButton_Clicked(object sender, EventArgs e)
    {
        ImageByteArray = new byte[0];
        ChosenImage.Source = null;
        if (ImageByteArray.Length > 0 && ImageByteArray != null)
        {

            ImageStack.IsVisible = true;
            AddImageButton.IsVisible = false;
        }
        else
        {
            ImageStack.IsVisible = false;
            AddImageButton.IsVisible = true;
        }
    }
}