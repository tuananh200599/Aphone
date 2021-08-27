using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aphone.ApiIntegration;
using Aphone.ViewModel.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                //Selected = Id.HasValue && Id.Value == x.Id

            });
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(categories);
        }
        [HttpGet]
        [Consumes("multipart/form-data")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _categoryApiClient.CreateCategory(request);
            if (result)
            {
                TempData["result"] = "Tạo mới thể loại thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", " Tạo thể loại thất bại");
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var category = await _categoryApiClient.GetById(id);
            var editVm = new CategoryUpdateRequest()
            {
                Id = category.Id,
                Description = category.Description,              
                Name = category.Name
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _categoryApiClient.UpdateCategory(request);
            if (result)
            {
                TempData["result"] = "Cập nhật thể loại thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật thể loại thất bại");
            return View(request);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new CategoryDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.DeleteCategory(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa thất bại ");
            return View(request);
        }
    }

}
