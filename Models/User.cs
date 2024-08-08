using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public async Task<bool> Login(string username, string password)
        {
            User user = new User
            {
                Username = username,
                Password = password
            };

            var UserID = await App.Service.Login(username, password);

            if (UserID != null && UserID.Success == true)
            {
                var GetUserByID = await App.Service.GetUserDetailsByID(UserID.Data);

                if (GetUserByID != null)
                {

                    Preferences.Clear();
                    Preferences.Set("UsersID", GetUserByID.Data.Id);
                    Preferences.Set("Username", GetUserByID.Data.Username);
                    return true;
                }
            }

            return false;

        }

        public async Task<ApiDataResponse<UserDetails>> Register(string username, string password)
        {
            var response = new ApiDataResponse<UserDetails>();

            User user = new User
            {
                Username = username,
                Password = password
            };

            response = await App.Service.Register(username, password);


            if (response.Success)
            {
                Preferences.Clear();
                Preferences.Set("UsersID", response.Data.Id);
                Preferences.Set("Username", response.Data.Username);
            }

            return response;
        }


    }


}
