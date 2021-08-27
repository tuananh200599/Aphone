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
        Task<List<CategoryVm>> GetAll();
        Task<CategoryVm> GetById(int id);
        Task<bool> CreateCategory(CategoryCreateRequest request);

        Task<bool> UpdateCategory(CategoryUpdateRequest request);

        Task<bool> DeleteCategory(int id);

    }
}
