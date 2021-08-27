using Aphone.ApiIntegration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aphone.WebApp.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public SideBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _categoryApiClient.GetAll();
            return View(items);
        }
    }
}
