using MyShopApp.Views;

namespace MyShopApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //var route = $"{nameof(Landing)}";

            //await Shell.Current.GoToAsync(route);
            //MainPage = AppShell();
            //await Navigation.PushModalAsync(new Landing());
        }
    }

}
