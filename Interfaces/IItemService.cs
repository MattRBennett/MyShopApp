﻿using MyShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
        Task<Item> GetItemByItemID(int ID);
        Task<List<ItemCategory>> GetItemCategories();
        Task<List<Item>> GetItemsByCategory(ItemCategory itemCategory);
        Task<ApiDataResponse<Item>> AddNewItem(Item NewItem);

        Task<ApiDataResponse<Item>> UpdateItem(Item UpdatedItem);
        Task<ApiDataResponse<Item>> DeleteItem(int ItemID);
    }
}
