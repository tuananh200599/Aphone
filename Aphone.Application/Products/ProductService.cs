using Aphone.Application.Common;
using Aphone.Data.EF;
using Aphone.Data.Entities;
using Aphone.ViewModel.Common;
using Aphone.ViewModel.ProductImages;
using Aphone.ViewModel.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly AphoneDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ProductService(AphoneDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;


        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
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

                if (request.ThumbnailImage != null)
                {
                    product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new AphoneException($"Cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }


        public async Task<PagedResult<ProductVm>> GetAllPaging(GetManageProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        //where p.CategoryId == request.CategoryId && pi.IsDefault == true
                        select new { p, pi };
            //2. filter
            //if (!string.IsNullOrEmpty(request.Keyword))
            //    query = query.Where(x => x.p.Name.Contains(request.Keyword));

            //if (request.CategoryId != null && request.CategoryId != 0)
            //{
            //    query = query.Where(p => p.p.CategoryId == request.CategoryId);
            //}
            int totalRow = await _context.Products.CountAsync();

            var data = await query.OrderByDescending(x => x.p.Id)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DatedCreate = x.p.DatedCreate,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    CategoryId = x.p.CategoryId,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;

        }

        public async Task<ProductVm> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            //var categories = await _context.Products.ToListAsync();
            var image = await _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
            var productViewModel = new ProductVm()
            {
                Id = product.Id,
                Name = product != null ? product.Name : null,
                DatedCreate = product.DatedCreate,
                Description = product != null ? product.Description : null,
                Details = product != null ? product.Details : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = product != null ? product.SeoAlias : null,
                SeoDescription = product != null ? product.SeoDescription : null,
                SeoTitle = product != null ? product.SeoTitle : null,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return productViewModel;
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new AphoneException($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new AphoneException($"Cannot find an image with id {imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) throw new Exception($"Cannot find a product with id: {request.Id}");
            product.Name = request.Name;
            product.Description = request.Description;
            //product.Price = request.Price;
            //product.OriginalPrice = request.OriginalPrice;
            product.Details = request.Details;
            //product.Stock = request.Stock;
            product.SeoAlias = request.SeoAlias;
            product.SeoDescription = request.SeoDescription;
            product.SeoTitle = request.SeoTitle;
            //product.CategoryId = request.CategoryId;
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }


    

    public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new AphoneException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                 .Select(i => new ProductImageViewModel()
                 {
                     Caption = i.Caption,
                     DateCreated = i.DateCreated,
                     FileSize = i.FileSize,
                     Id = i.Id,
                     ImagePath = i.ImagePath,
                     IsDefault = i.IsDefault,
                     ProductId = i.ProductId,
                     SortOrder = i.SortOrder
                 }).ToListAsync();
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var user = await _context.Products.FindAsync(id);
            if (user == null)
            {
                return new ApiErrorResult<bool>($"Product with id {id} not exist");
            }

            foreach (var category in request.Categories)
            {
                var productInCategory = await _context.Products
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)
                    && x.Id == id);

                if (productInCategory != null && category.Selected == false)
                {
                    _context.Products.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected == true)
                {
                    await _context.Products.AddAsync(new Product()
                    {
                        CategoryId = int.Parse(category.Id),
                        Id = id
                    });
                }
            }

            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<ProductVm>> GetFeaturedProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                            //where p.CategoryId == request.CategoryId && pi.IsDefault == true
                        select new { p, pi };

            var data = await query.OrderByDescending(x => x.p.Stock).Take(take)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DatedCreate = x.p.DatedCreate,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    CategoryId = x.p.CategoryId,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }

        public async Task<List<ProductVm>> GetLatestProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                            //where p.CategoryId == request.CategoryId && pi.IsDefault == true
                        select new { p, pi };

            var data = await query.OrderByDescending(x => x.p.DatedCreate).Take(take)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DatedCreate = x.p.DatedCreate,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    CategoryId = x.p.CategoryId,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }

        public async Task<List<ProductVm>> GetSpecialProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                            //where p.CategoryId == request.CategoryId && pi.IsDefault == true
                        select new { p, pi };

            var data = await query.OrderByDescending(x => x.p.Name).Take(take)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DatedCreate = x.p.DatedCreate,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    CategoryId = x.p.CategoryId,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }

        public async Task<List<ProductVm>> GetRoyalProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                            //where p.CategoryId == request.CategoryId && pi.IsDefault == true
                        select new { p, pi };

            var data = await query.OrderByDescending(x => x.p.Price).Take(take)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DatedCreate = x.p.DatedCreate,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    CategoryId = x.p.CategoryId,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }
        public async Task<List<ProductVm>> GetRoyaledProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                            //where p.CategoryId == request.CategoryId && pi.IsDefault == true
                        select new { p, pi };

            var data = await query.OrderByDescending(x => x.p.Id).Take(take)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    DatedCreate = x.p.DatedCreate,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.p.SeoAlias,
                    SeoDescription = x.p.SeoDescription,
                    SeoTitle = x.p.SeoTitle,
                    Stock = x.p.Stock,
                    CategoryId = x.p.CategoryId,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }
    }
}
