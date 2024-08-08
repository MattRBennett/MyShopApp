using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Item Name";
        public string Description { get; set; } = "New Item Description";
        public decimal Price { get; set; } = decimal.Zero;
        public byte[] Image { get; set; } = new byte[0];
        public ItemCategory ItemsCategory { get; set; } = ItemCategory.Unassigned;

        public async Task<List<Item>> GetItems()
        {
            List<Item> items = new();
            var itemsindb = await App.Service.GetAllItems();
            //items.AddRange(itemsindb);
            
            return itemsindb;
        }

        public async Task<Item> GetItemByItemID(int ID)
        {
            Item item = await App.Service.GetItemByItemID(ID);
            return item;
        }

        public async Task AddNewItem(Item NewItem)
        {
            await App.Service.AddNewItem(NewItem);
        }

        public async Task UpdateItem(Item UpdatedItem)
        {
            await App.Service.UpdateItem(UpdatedItem);
        }

        public async Task DeleteItem(int ItemID)
        {
            await App.Service.DeleteItem(ItemID);
        }

        public async Task<List<CategoryList>> GetAllCategoryItems()
        {
            List<CategoryList> NewCategoryItem = new();
            var CategoryItems = await App.Service.GetItemCategories();

            foreach (var item in CategoryItems)
            {
                ImageSource image;

                switch (item)
                {
                    case ItemCategory.Unassigned:
                        image = ImageSource.FromFile("unassigned.png");
                        break;
                    case ItemCategory.Appliances:
                        image = ImageSource.FromFile("appliances.png");
                        break;
                    case ItemCategory.Electronics:
                        image = ImageSource.FromFile("electronics.png");
                        break;
                    case ItemCategory.Fashion:
                        image = ImageSource.FromFile("fashion.png");
                        break;
                    case ItemCategory.Health:
                        image = ImageSource.FromFile("health.png");
                        break;
                    case ItemCategory.Sports:
                        image = ImageSource.FromFile("sports.png");
                        break;
                    case ItemCategory.Media:
                        image = ImageSource.FromFile("media.png");
                        break;
                    case ItemCategory.Toys:
                        image = ImageSource.FromFile("toys.png");
                        break;
                    case ItemCategory.Furniture:
                        image = ImageSource.FromFile("furniture.png");
                        break;
                    case ItemCategory.Groceries:
                        image = ImageSource.FromFile("groceries.png");
                        break;
                    case ItemCategory.Automotive:
                        image = ImageSource.FromFile("automotive.png");
                        break;
                    case ItemCategory.Outdoors:
                        image = ImageSource.FromFile("outdoors.png");
                        break;
                    default:
                        image = ImageSource.FromFile("unassigned.png");
                        break;

                }

                NewCategoryItem.Add(new CategoryList
                {
                    CategoryID = (int)item,
                    CategoryName = item.ToString(),
                    Image = image
                });
            }

            return NewCategoryItem;
        }

        public async Task<byte[]> AddImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                return imageBytes;
            }
            else
            {
                return new byte[0];
            }
        }
    }

}
