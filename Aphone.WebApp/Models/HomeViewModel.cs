using Aphone.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aphone.WebApp.Models
{
    public class HomeViewModel
    {
        public List<ProductVm> FeaturedProducts { get; set; }
        public List<ProductVm> LatestProducts { get; set; }
        public List<ProductVm> SpecialProducts { get; set; }
    }
}
