using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aphone.ApiIntegration;
using Aphone.ViewModel.Common;
using Aphone.ViewModel.Products;
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
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                
                SpecialProducts = await _productApiClient.GetSpecialProducts(SystemConstants.ProductSettings.NumberOfSpecialProducts),
                
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var products = await _productApiClient.GetAllPagings(new GetManageProductPagingRequest()
            {
                CategoryId = id,
                PageIndex = page,
                PageSize = 10
            });
            return View(new ProductCategoryViewModel()
            {
                Category = await _categoryApiClient.GetById( id),
                Products = products
            }); ;
        }
    }
}
