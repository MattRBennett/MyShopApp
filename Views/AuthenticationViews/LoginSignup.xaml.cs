using MyShopApp.Models;

namespace MyShopApp.Views.AuthenticationViews;

public partial class LoginSignup : ContentPage
{
	public LoginSignup()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		User user = new User();

		if (UsernameEntry.Text != null && PasswordEntry.Text != null)
		{

			//var login = await user.Login

            var UserLogin = 
        }
		else
		{
			return;
		}
    }
}