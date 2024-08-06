using MyShopApp.Interfaces;
using MyShopApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using static MyShopApp.Services.ItemService;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyShopApp.Services
{
    public class ItemService : IItemService
    {

        public List<Item> items { get; private set; }
        public List<ItemCategory> categories { get; private set; }


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


        public async Task<List<Item>> GetAllItems()
        {
            items = new List<Item>();
            ApiListResponse<Item> apiListResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/GetAllItems"));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    apiListResponse = JsonConvert.DeserializeObject<ApiListResponse<Item>>(content);

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
                //throw new Exception(ex.Message);
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


        public async Task<List<ItemCategory>> GetItemCategories()
        {
            categories = new List<ItemCategory>();
            ApiListResponse<ItemCategory> apiListResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/GetItemCategories"));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    apiListResponse = JsonConvert.DeserializeObject<ApiListResponse<ItemCategory>>(content);

                    if (apiListResponse != null)
                    {
                        categories = apiListResponse.Data;
                        apiListResponse.Success = true;
                        apiListResponse.Message = "All categories returned successfully!";
                    }
                    else
                    {
                        apiListResponse.Success = false;
                        apiListResponse.Message = "Failed to return all categories!";
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
                //throw new Exception(ex.Message);
            }

            return categories;
        }

        public async Task<List<Item>> GetItemsByCategory(ItemCategory itemCategory)
        {
            items = new List<Item>();
            ApiListResponse<Item> apiListResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/GetItemsByCategory?itemCategory={itemCategory}"));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    apiListResponse = JsonConvert.DeserializeObject<ApiListResponse<Item>>(content);

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
                //throw new Exception(ex.Message);
            }

            return items;
        }

        public async Task<ApiDataResponse<Item>> AddNewItem(Item NewItem)
        {
            ApiDataResponse<Item> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/AddNewItem"));

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, NewItem);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"New item successfully saved.");

                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "Item has been successfully added.";
                }
                else
                {
                    Debug.WriteLine($"Failed to add new item. Status code: {response.StatusCode}");

                    apiDataResponse.Success = false;
                    apiDataResponse.Message = "Failed to add new item. Status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while adding new item: {ex.Message}");
            }

            return apiDataResponse;
        }

        public async Task<ApiDataResponse<Item>> UpdateItem(Item UpdatedItem)
        {
            ApiDataResponse<Item> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/UpdateItem"));

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(uri, UpdatedItem);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item has been successfully updated.");

                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "Item has been successfully updated.";
                }
                else
                {
                    Debug.WriteLine($"Failed to update item. Status code: {response.StatusCode}");

                    apiDataResponse.Success = false;
                    apiDataResponse.Message = "Failed to update item. Status code: {response.StatusCode}";
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while updating item: {ex.Message}");
            }

            return apiDataResponse;
        }

        public async Task<ApiDataResponse<Item>> DeleteItem(int ItemID)
        {
            ApiDataResponse<Item> apiDataResponse = new();

            Uri uri = new(string.Format($"{Constants.apiURL}Item/DeleteItem?Id={ItemID}"));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Item has been successfully deleted.");

                    apiDataResponse.Success = true;
                    apiDataResponse.Message = "Item has been successfully updated.";
                }
                else
                {
                    Debug.WriteLine($"Failed to delete item. Status code: {response.StatusCode}");

                    apiDataResponse.Success = false;
                    apiDataResponse.Message = "Failed to delete item. Status code: {response.StatusCode}";

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while deleting item: {ex.Message}");
            }

            return apiDataResponse;
        }
    }
}
