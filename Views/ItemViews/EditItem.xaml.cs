using MyShopApp.Models;

namespace MyShopApp.Views.ItemViews;

public partial class EditItem : ContentPage
{
    List<ItemCategory> categories = new();
    Item item = new();
    public byte[] ImageByteArray;
    public int ItemID;
    public EditItem(int SelectedItemsID)
	{
		InitializeComponent();
        ItemID = SelectedItemsID;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        item = await item.GetItemByItemID(ItemID);
        var FetchedCategories = await App.Service.GetItemCategories();
        categories.AddRange(FetchedCategories);
        CategoryOfItem.ItemsSource = categories;

        if (item != null)
        {
            NameOfItem.Text = item.Name;
            DescriptionOfItem.Text = item.Description;
            decimal ItemsPrice = item.Price;
            PriceOfItem.Text = ItemsPrice.ToString();
            int CategoryIndex = FetchedCategories.IndexOf(item.ItemsCategory);
            CategoryOfItem.SelectedIndex = CategoryIndex + 1;
            ChosenImage.Source = item.Image != null && item.Image.Length > 0
            ? ImageSource.FromStream(() => new MemoryStream(item.Image))
            : null;
            ImageByteArray = item.Image;

        }
        
        

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

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var selectedCategory = (ItemCategory)CategoryOfItem.SelectedItem;

        Item UpdatedDetails = new Item
        {
            Id = ItemID,
            Name = NameOfItem.Text,
            Description = DescriptionOfItem.Text,
            Price = decimal.Parse(PriceOfItem.Text),
            Image = ImageByteArray,
            ItemsCategory = selectedCategory
        };

        await item.UpdateItem(UpdatedDetails);
        await Navigation.PopModalAsync();
    }


    private async void AddImageButton_Clicked(object sender, EventArgs e)
    {
        ImageByteArray = await item.AddImage();

        ChosenImage.Source = ImageByteArray != null && ImageByteArray.Length > 0
    ? ImageSource.FromStream(() => new MemoryStream(ImageByteArray))
    : null;

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