using MyShopApp.Models;

namespace MyShopApp.Views.ItemViews;

public partial class ViewItemsByCategory : ContentPage
{
	public CategoryList ItemsCategory;

	public List<PresentableItems> items = new();

    public class PresentableItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public ImageSource Image { get; set; }
    }
	public ViewItemsByCategory(CategoryList ItemCategory)
	{
		InitializeComponent();

        LoadingIndicatorStack.IsVisible = true;
        Content.IsVisible = false;
        NothingFound.IsVisible = false;
        ItemsCategory = ItemCategory;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        
        
        items.Clear();

        CategoryNameLabel.Text = ItemsCategory.CategoryName;

        ItemCategory category = (ItemCategory)ItemsCategory.CategoryID;

        var GetItems = await App.Service.GetItemsByCategory(category);
        if (GetItems.Count > 0)
        {
            foreach (var item in GetItems)
            {
                var imageSource = item.Image != null && item.Image.Length > 0
                ? ImageSource.FromStream(() => new MemoryStream(item.Image))
                : null;

                items.Add(new PresentableItems
                {
                    Id = item.Id,
                    Name = item.Name,
                    ItemCategory = item.ItemsCategory,
                    
                    Image = imageSource,
                    Price = $"£{item.Price}"
                });
            }
            //items.AddRange(GetItems);
            ItemsCollectionView.ItemsSource = items;
            Content.IsVisible = true;
        }
		else
        {
            NothingFound.IsVisible = true;
        }

        LoadingIndicatorStack.IsVisible = false;
        Content.IsVisible = true;
    }

    private async void ItemFrame_Tapped(object sender, TappedEventArgs e)
    {
		if (sender is Frame frame && frame.BindingContext is PresentableItems item) 
		{
			await Navigation.PushModalAsync(new ViewItem(item.Id));
		}
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddNewItem());
    }
}