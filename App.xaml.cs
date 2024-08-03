using MyShopApp.Services;
using MyShopApp.Views;

namespace MyShopApp
{
    public partial class App : Application
    {
        public static ServiceManager Service { get; set; }
        public App()
        {
            InitializeComponent();

            Service = new ServiceManager(new ItemService());

            MainPage = new MainPage();
            //MainPage = new AppShell();
        }
    }
}
