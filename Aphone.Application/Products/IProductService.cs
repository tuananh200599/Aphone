using Aphone.ViewModel.Common;
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
        Task<List<ProductVm>> GetAllPaging(GetManageProductPagingRequest request);
        Task<ProductVm> GetById(int productId);

    }
}
