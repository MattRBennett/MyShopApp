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
            Routing.RegisterRoute(nameof(AddNewItem), typeof(AddNewItem));
            Routing.RegisterRoute(nameof(DeleteItem), typeof(DeleteItem));
            Routing.RegisterRoute(nameof(ViewAllCategories), typeof(ViewAllCategories));
            Routing.RegisterRoute(nameof(ViewItem), typeof(ViewItem));
            Routing.RegisterRoute(nameof(ViewItemsByCategory), typeof(ViewItemsByCategory));



        }
    }
}
