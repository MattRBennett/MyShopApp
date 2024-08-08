using MyShopApp.Models;
using System.Collections.ObjectModel;

namespace MyShopApp.Views.ItemViews;

public partial class ViewAllItems : ContentPage
{
	List<Item> items = new();
    public List<PresentableItems> presentableItems = new();

    public class PresentableItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public ImageSource Image { get; set; }
    }
    public ViewAllItems()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
	{
		base.OnAppearing();

        Item item = new();
		items = await App.Service.GetAllItems();

        foreach (var fetcheditems in items)
        {
            var imageSource = fetcheditems.Image != null && fetcheditems.Image.Length > 0
            ? ImageSource.FromStream(() => new MemoryStream(fetcheditems.Image))
            : null;

            presentableItems.Add(new PresentableItems
            {
                Id = fetcheditems.Id,
                Name = fetcheditems.Name,
                ItemCategory = fetcheditems.ItemsCategory,
                Image = imageSource,
                Price = $"£{fetcheditems.Price}"
            });
        }
        ItemsCollecitonView.ItemsSource = items;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync("..");
		await Navigation.PopModalAsync();

    }

    private async void ItemTapped(object sender, TappedEventArgs e)
    {
		if (sender is Frame frame && frame.BindingContext is Item item) 
		{
			await Navigation.PushModalAsync(new ViewItem(item.Id));
		}
    }
    private async void ItemFrame_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is PresentableItems item)
        {
            await Navigation.PushModalAsync(new ViewItem(item.Id));
        }
    }
}