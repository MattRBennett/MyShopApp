using MyShopApp.Views;
using MyShopApp.Views.ItemViews;

namespace MyShopApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Landing), typeof(Landing));
            Routing.RegisterRoute(nameof(ViewAllItems), typeof(ViewAllItems));

        }
    }
}
