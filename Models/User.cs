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

        public async Task<ApiDataResponse<int>> Login(string username, string password)
        {
            User user = new User
            {
                Username = username,
                Password = password
            };

            var UserID = await App.Service.Login(username, password);


            if (UserCredentials != null)
            {
                var GetUserByID = await 
            }

            return UserLogin;
        }


    }

    
}
