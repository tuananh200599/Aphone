using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aphone.ApiIntegration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aphone.AdminApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IConfiguration _configuration;
        

        public CategoryController(ICategoryApiClient categoryApiClient,
             IConfiguration configuration)
        {
            _categoryApiClient = categoryApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index( )
        {

            var categories = await _categoryApiClient.GetAll();
            
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(categories);
        }
    }

}
