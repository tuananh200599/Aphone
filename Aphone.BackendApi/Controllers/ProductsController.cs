using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aphone.Application.Products;
using Aphone.ViewModel.ProductImages;
using Aphone.ViewModel.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aphone.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }
        [HttpGet("{productId}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _productService.GetById(productId);
            if (product == null)
                return BadRequest("Không tìm thấy sản phẩm");
            return Ok(product);
        }
        [HttpGet("featured/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeaturedProducts(int take)
        {
            var products = await _productService.GetFeaturedProducts( take);
            return Ok(products);
        }
        [HttpGet("latest/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLatestProducts(int take)
        {
            var products = await _productService.GetLatestProducts( take);
            return Ok(products);
        }
        [HttpGet("special/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSpecialProducts(int take)
        {
            var products = await _productService.GetSpecialProducts(take);
            return Ok(products);
        }
        [HttpGet("royal/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoyalProducts(int take)
        {
            var products = await _productService.GetRoyalProducts(take);
            return Ok(products);
        }
        [HttpGet("royaled/{take}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRoyaledProducts(int take)
        {
            var products = await _productService.GetRoyaledProducts(take);
            return Ok(products);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest();

            var product = await _productService.GetById(productId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }
        [HttpPut("{productId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = productId;
            var affectedResult = await _productService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _productService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Không tìm thấy sản phẩm");
            return Ok(image);
        }
        [HttpPut("{id}/categories")]
        [Authorize]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
