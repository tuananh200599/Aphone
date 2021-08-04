using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aphone.WebApp.Models;
using Aphone.ApiIntegration;
using Aphone.ViewModel.Common;

namespace Aphone.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger,
            IProductApiClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedProducts = await _productApiClient.GetFeaturedProducts( SystemConstants.ProductSettings.NumberOfFeaturedProducts),
                LatestProducts = await _productApiClient.GetLatestProducts( SystemConstants.ProductSettings.NumberOfLatestProducts),
                SpecialProducts = await _productApiClient.GetLatestProducts(SystemConstants.ProductSettings.NumberOfSpecialProducts)
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
