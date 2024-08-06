using MyShopApp.Services;
using MyShopApp.Views;
using MyShopApp.Views.AuthenticationViews;

namespace MyShopApp
{
    public partial class App : Application
    {
        public static ServiceManager Service { get; set; }
        public App()
        {
            InitializeComponent();

            Service = new ServiceManager(new ItemService(), new AuthenticationService());

            MainPage = new LoginSignup();
            //MainPage = new AppShell();
        }
    }
}
