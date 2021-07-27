﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aphone.Application.Categories;
using Aphone.ViewModel.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Aphone.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("categoryId")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            var product = await _categoryService.GetById(categoryId);
            if (product == null)
                return BadRequest("Cannot find category");
            return Ok(product);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        //[Authorize]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryId = await _categoryService.Create(request);
            if (categoryId == 0)
                return BadRequest();

            var category = await _categoryService.GetById(categoryId);

            return CreatedAtAction(nameof(GetById), new { id = categoryId }, category);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpPut]
        [Consumes("application/json")]

        public async Task<IActionResult> Update(int categoryId, CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = categoryId;
            var affectedResult = await _categoryService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int categoryId)
        {
            var affectedResult = await _categoryService.Delete(categoryId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
