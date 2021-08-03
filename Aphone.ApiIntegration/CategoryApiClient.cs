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

        public async Task<List<CategoryVm>> GetAll()
        {
            var categories = await GetAsync <List<CategoryVm>>($"/api/categories");
            return categories;
        }
    

        public async Task<CategoryVm> GetById(int id)
        {
            return await GetAsync<CategoryVm>($"/api/categories/{id}");
        }

        public Task<int> Update(int categoryId, CategoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
