using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MyShopApp
{
    public static class Constants
    {

        //public static string apiURL = "";
        public static string apiURL = DeviceInfo.Platform == DevicePlatform.WinUI ? "https://localhost:7257/api/" : DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5163/api/" : "https://localhost:5163/api/";
    }
}
