using Aphone.ViewModel.Categories;
using Aphone.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.ApiIntegration
{
    public interface ICategoryApiClient
    {
        Task<PagedResult<CategoryVm>> GetAll();
        Task<CategoryVm> GetById( int categoryId);
        Task<int> Create(CategoryCreateRequest request);

        Task<int> Update(CategoryUpdateRequest request);

        Task<int> Delete(int categoryId);

    }
}
