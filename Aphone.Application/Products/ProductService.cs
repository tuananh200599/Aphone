using Aphone.Data.EF;
using Aphone.Data.Entities;
using Aphone.ViewModel.Common;
using Aphone.ViewModel.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly AphoneDbContext _context;
        public ProductService(AphoneDbContext context)
        {
            _context = context;
            
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            {

                var product = new Product ()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    OriginalPrice = request.OriginalPrice,
                    Details = request.Details,
                    Stock = request.Stock,
                    SeoAlias = request.SeoAlias,
                    SeoDescription = request.SeoDescription,
                    SeoTitle = request.SeoTitle,
                    CategoryId = request.CategoryId

                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception($"Cannot find a product: {productId}");
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }


        public async Task<List<ProductVm>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //int totalRow = await _context.Products.CountAsync();

            var products = await _context.Products.OrderByDescending(x => x.Id)
                //.Skip((request.PageIndex - 1) * request.PageSize)
                //.Take(request.PageSize)
                .Select(x => new ProductVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DatedCreate = x.DatedCreate,
                    Description = x.Description,
                    Details = x.Details,
                    OriginalPrice = x.OriginalPrice,
                    Price = x.Price,
                    SeoAlias = x.SeoAlias,
                    SeoDescription = x.SeoDescription,
                    SeoTitle = x.SeoTitle,
                    Stock = x.Stock,
                    CategoryId = x.CategoryId
                    //ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();
            return products;

            //var pagedResult = new PagedResult<ProductVm>()
            //{
            //    TotalRecords = totalRow,
            //    PageSize = request.PageSize,
            //    PageIndex = request.PageIndex,
            //    Items = data
            //};
            //return pagedResult;

        }

        public async Task<ProductVm> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            //var categories = await _context.Products.ToListAsync();
            var productViewModel = new ProductVm()
            {
                Id = product.Id,
                Name = product.Name,
                DatedCreate = product.DatedCreate,
                Description = product.Description,
                Details = product.Details,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = product.SeoAlias,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
            };
            return productViewModel;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) throw new Exception($"Cannot find a product with id: {request.Id}");
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.OriginalPrice = request.OriginalPrice;
            product.Details = request.Details;
            product.Stock = request.Stock;
            product.CategoryId = request.CategoryId;
            return await _context.SaveChangesAsync();

        }
    }
}
