﻿using MyShopApp.Interfaces;
using MyShopApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Services
{
    public class CartService : ICartServices
    {
        readonly HttpClient client;

        public CartService()
        {
            client = new HttpClient();
        }
        public async Task<ApiDataResponse<Cart>> AddCart(Cart NewCart)
        {
            ApiDataResponse<Cart> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Cart/AddNewCart"));

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, NewCart);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"New cart successfully saved.");

                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "Cart has been successfully added.";
                }
                else
                {
                    Debug.WriteLine($"Failed to add new cart. Status code: {response.StatusCode}");

                    apiDataResponse.Success = false;
                    apiDataResponse.Message = "Failed to add new cart. Status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while adding new cart: {ex.Message}");
            }

            return apiDataResponse;
        }

        

        public async Task<ApiDataResponse<Cart>> GetCartByUserID(int UserID)
        {
            ApiDataResponse<Cart> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Cart/GetCartByUserID?UserID={UserID}"));

            Cart cart;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<Cart>>(content);

                    if (apiDataResponse != null)
                    {
                        cart = apiDataResponse.Data;
                        apiDataResponse.Success = true;
                        apiDataResponse.Message = "Cart data returned successfully!";
                    }
                    else
                    {
                        apiDataResponse.Success = false;
                        apiDataResponse.Message = "Failed to return cart data!";
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

        public async Task<ApiDataResponse<Cart>> AddNewCart(Cart NewCart)
        {
            ApiDataResponse<Cart> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Cart/AddNewCart"));

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, NewCart);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"New cart successfully saved.");

                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "Cart has been successfully added.";
                }
                else
                {
                    Debug.WriteLine($"Failed to add new cart. Status code: {response.StatusCode}");

                    apiDataResponse.Success = false;
                    apiDataResponse.Message = "Failed to add new cart. Status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while adding new cart: {ex.Message}");
            }

            return apiDataResponse;
        }
    }
}

