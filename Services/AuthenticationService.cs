using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MyShopApp.Interfaces;
using MyShopApp.Models;
using Newtonsoft.Json;

namespace MyShopApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly HttpClient client;

        public AuthenticationService()
        {
            client = new HttpClient();
        }

        public async Task<ApiDataResponse<int>> Login(string username, string password)
        {
            ApiDataResponse<int> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Auth/Login"));

            User credentials = new User { Username = username, Password = password };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, credentials);

                string content = await response.Content.ReadAsStringAsync();

                apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<int>>(content);

                if (response.IsSuccessStatusCode)
                {
                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "User has logged in successfully!";
                }
                else
                {
                    Console.WriteLine($"Error {response.StatusCode} : {response.RequestMessage}");

                    apiDataResponse.Success = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return apiDataResponse;
        }

        public async Task<ApiDataResponse<UserDetails>> Register(string username, string password)
        {
            ApiDataResponse<UserDetails> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Auth/Register"));

            User credentials = new User { Username = username, Password = password };

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, credentials);

                string content = await response.Content.ReadAsStringAsync();

                apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<UserDetails>>(content);

                if (response.IsSuccessStatusCode)
                {
                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "User has registered in successfully!";
                }
                else
                {
                    Console.WriteLine($"Error {response.StatusCode} : {response.RequestMessage}");

                    apiDataResponse.Success = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return apiDataResponse;
        }

        public async Task<ApiDataResponse<UserDetails>> GetUserDetailsByID(int ID)
        {
            ApiDataResponse<UserDetails> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}User/GetUserDetailsByID?UsersID={ID}"));

            UserDetails userDetails;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<UserDetails>>(content);

                    if (apiDataResponse != null)
                    {
                        userDetails = apiDataResponse.Data;
                        apiDataResponse.Success = true;
                        apiDataResponse.Message = "User data returned successfully!";
                    }
                    else
                    {
                        apiDataResponse.Success = false;
                        apiDataResponse.Message = "Failed to return user data!";
                    }
                }
                else
                {
                    Console.WriteLine($"Error {response.StatusCode} : {response.RequestMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

            return apiDataResponse;
        }
    }
}
