using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.ViewModel.Common
{
    public class SystemConstants
    {
        public const string MainConnectionString = "AphoneDb";
        public const string CartSession = "CartSession";
        public class AppSettings
        {
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }
        public class ProductSettings
        {
            public const int NumberOfFeaturedProducts = 12;
            public const int NumberOfLatestProducts = 12;
            public const int NumberOfSpecialProducts = 12;
        }
    }
}
