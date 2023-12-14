using SportShop.@base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SportShop
{
    public enum TableName
    { 
        Categories,
        Product,
        Providers,
        Roles,
        Size,
        Users,
    }
    internal class AppData
    {
        public static Frame frameAuth;
        public static Frame frameUser;

        public static SportsShopEntities db = new SportsShopEntities();

        public static Users CurrentUser;
    }
}
