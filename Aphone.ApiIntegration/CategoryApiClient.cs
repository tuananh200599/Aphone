using Aphone.ViewModel.Categories;
using Aphone.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.ApiIntegration
{
    public class CategoryApiClient : BaseApiClient,ICategoryApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
                    IHttpContextAccessor httpContextAccessor,
                     IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
            public Task<int> Create(CategoryCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<CategoryVm>> GetAll()
        {
            var categories = await GetAsync <PagedResult<CategoryVm>>($"/api/categories");
            return categories;
        }
    

        public async Task<CategoryVm> GetById(int categoryId)
        {
            return await GetAsync<CategoryVm>($"/api/categories/{categoryId}");
        }

        public Task<int> Update(CategoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
