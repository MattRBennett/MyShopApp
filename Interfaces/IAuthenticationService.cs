﻿using MyShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopApp.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiDataResponse<int>> Login(string username, string password);
    }
}
