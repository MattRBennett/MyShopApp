using MyShopApp.Interfaces;
using MyShopApp.Models;
using MyShopApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Services
{
    public class ServiceManager
    {

        
        readonly IItemService itemServices;
        readonly IAuthenticationService authenticationServices;

        public ServiceManager(IItemService itemService, IAuthenticationService authenticationService)
        {
            itemServices = itemService;
            authenticationServices = authenticationService;
        }

        #region ItemService
        public Task<List<Item>> GetAllItems()
        {
            return itemServices.GetAllItems();
        }

        public Task<Item> GetItemByItemID(int ID)
        {
            return itemServices.GetItemByItemID(ID);
        }

        public Task<List<ItemCategory>> GetItemCategories()
        {
            return itemServices.GetItemCategories();
        }

        public Task<List<Item>> GetItemsByCategory(ItemCategory itemCategory)
        {
            return itemServices.GetItemsByCategory(itemCategory);
        }

        public Task<ApiDataResponse<Item>> AddNewItem(Item NewItem)
        {
            return itemServices.AddNewItem(NewItem);
        }

        public Task<ApiDataResponse<Item>> UpdateItem(Item UpdatedItem)
        {
            return itemServices.UpdateItem(UpdatedItem);
        }

        public Task<ApiDataResponse<Item>> DeleteItem(int ItemID)
        {
            return itemServices.DeleteItem(ItemID);
        }

        #endregion

        #region AuthenticationService

        

        public Task<ApiDataResponse<int>> Login(string username, string password)
        {
            return authenticationServices.Login(username, password);
        }

        #endregion
    }
}
