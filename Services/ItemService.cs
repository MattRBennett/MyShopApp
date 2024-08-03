using MyShopApp.Interfaces;
using MyShopApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using static MyShopApp.Services.ItemService;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyShopApp.Services
{

    public class ItemService : IItemService
    {
        public class ApiListResponse
        {
            public List<Item> Data { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public class ApiDataResponse<T>
        {
            public T? Data { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        readonly HttpClient client;
        readonly System.Text.Json.JsonSerializerOptions serializerOptions;
        public ItemService()
        {
            client = new HttpClient();

            serializerOptions = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = true
            };
        }

        public List<Item> items { get; private set; }
        public async Task<List<Item>> GetAllItems()
        {
            items = new List<Item>();
            ApiListResponse apiListResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/GetAllItems"));

            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    apiListResponse = JsonConvert.DeserializeObject<ApiListResponse>(content);

                    if (apiListResponse != null)
                    {
                        items = apiListResponse.Data;
                        apiListResponse.Success = true;
                        apiListResponse.Message = "All items returned successfully!";
                    }
                    else
                    {
                        apiListResponse.Success = false;
                        apiListResponse.Message = "Failed to return all items!";
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

            return items;
        }

        public async Task<Item> GetItemByItemID(int ID)
        {
            Item item = new();
            var serviceResponse = new ApiDataResponse<Item>();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/{ID}"));

            try
            {

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    serviceResponse = JsonConvert.DeserializeObject<ApiDataResponse<Item>>(content);

                    if (serviceResponse != null)
                    {
                        item = serviceResponse.Data;
                        serviceResponse.Success = true;
                        serviceResponse.Message = "All items returned successfully!";
                    }
                    else
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Failed to return all items!";
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

            return item;
        }
    }
}
