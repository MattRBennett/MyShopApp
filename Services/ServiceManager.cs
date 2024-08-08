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
        readonly ICartServices cartServices;

        public ServiceManager(IItemService itemService, IAuthenticationService authenticationService, ICartServices cartService)
        {
            itemServices = itemService;
            authenticationServices = authenticationService;
            cartServices = cartService;
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

        public Task<ApiDataResponse<UserDetails>> GetUserDetailsByID(int ID)
        {
            return authenticationServices.GetUserDetailsByID(ID);
        }

        public Task<ApiDataResponse<UserDetails>> Register(string username, string password)
        {
            return authenticationServices.Register(username, password);
        }

        #endregion

        #region CartService

        public Task<ApiDataResponse<Cart>> AddCart(Cart NewCart)
        {
            return cartServices.AddCart(NewCart);
        }
        public Task<ApiDataResponse<Cart>> GetCartByUserID(int UserID)
        {
            return cartServices.GetCartByUserID(UserID);
        }

        public Task<ApiDataResponse<Cart>> AddNewCart(Cart NewCart)
        {
            return cartServices.AddNewCart(NewCart);
        }

        public Task<ApiDataResponse<Cart>> RemoveCartItem(int UserID, int ItemID)
        {
            return cartServices.RemoveCartItem(UserID, ItemID);
        }

        #endregion
    }
}
