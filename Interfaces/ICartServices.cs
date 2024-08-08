using MyShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Interfaces
{
    public interface ICartServices
    {
        Task<ApiDataResponse<Cart>> AddCart(Cart NewCart);
        Task<ApiDataResponse<Cart>> GetCartByUserID(int UserID);
        Task<ApiDataResponse<Cart>> AddNewCart(Cart NewCart);
    }
}
