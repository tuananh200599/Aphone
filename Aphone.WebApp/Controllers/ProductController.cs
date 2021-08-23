using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aphone.ApiIntegration;
using Aphone.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aphone.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productApiClient.GetById(id);
            return View(new ProductDetailViewModel()
            {
                Product = product,

            });
        }
    }
}
