using Aphone.ViewModel.Common;
using Aphone.ViewModel.ProductImages;
using Aphone.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.Application.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);
        Task<PagedResult<ProductVm>> GetAllPaging(GetManageProductPagingRequest request);
        Task<ProductVm> GetById(int productId);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<List<ProductImageViewModel>> GetListImages(int productId);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<List<ProductVm>> GetFeaturedProducts( int take);
        Task<List<ProductVm>> GetLatestProducts( int take);
        Task<List<ProductVm>> GetSpecialProducts(int take);
        Task<List<ProductVm>> GetRoyalProducts(int take);
        Task<List<ProductVm>> GetRoyaledProducts(int take);

    }
}
