using MyShopApp.Models;

namespace MyShopApp.Views.AuthenticationViews;

public partial class LoginSignup : ContentPage
{
	public LoginSignup()
	{
		InitializeComponent();
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
		User user = new User();

		if (UsernameEntry.Text != null && PasswordEntry.Text != null)
		{
			bool LoginAttempt = await user.Login(UsernameEntry.Text, PasswordEntry.Text);

			if (LoginAttempt)
			{
				await Navigation.PushModalAsync(new Landing());
			}
			else
			{
				await DisplayAlert("Login failed", "Incorrect credentials, please try again.", "Ok");
				return;
			}
        }
		else
		{
			return;
		}
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    private async void ContinueButton_Clicked(object sender, EventArgs e)
    {
		Preferences.Clear();
		await Navigation.PushModalAsync(new Landing());
    }

    private async void SignupButton_Clicked(object sender, EventArgs e)
    {
        User user = new User();

        if (UsernameEntry.Text != null && PasswordEntry.Text != null)
        {
            var response = await user.Register(UsernameEntry.Text, PasswordEntry.Text);

            if (response.Success)
            {

                await Navigation.PushModalAsync(new Landing());
            }
            else
            {
                await DisplayAlert("Signup Failed", "A user with that account already exists.", "Ok");
                return;
            }
        }
        else
        {
            return;
        }
    }
}