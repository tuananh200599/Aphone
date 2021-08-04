using Aphone.ViewModel.Common;
using Aphone.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductVm>> GetAllPagings(GetManageProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(ProductUpdateRequest request);
        Task<bool> DeleteProduct(int id);
        Task<ProductVm> GetById(int id);
        Task<List<ProductVm>> GetFeaturedProducts( int take);
        Task<List<ProductVm>> GetLatestProducts( int take);
        Task<List<ProductVm>> GetSpecialProducts(int take);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
 
    }
}
