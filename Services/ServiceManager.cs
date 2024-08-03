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

        public ServiceManager(IItemService itemService)
        {
            itemServices = itemService;
        }

        public Task<List<Item>> GetAllItems()
        {
            return itemServices.GetAllItems();
        }

        public Task<Item> GetItemByItemID(int ID)
        {
            return itemServices.GetItemByItemID(ID);
        }
    }
}
