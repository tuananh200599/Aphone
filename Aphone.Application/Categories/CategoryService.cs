using Aphone.Data.EF;
using Aphone.Data.Entities;
using Aphone.ViewModel.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aphone.Application.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly AphoneDbContext _context;
        public CategoryService(AphoneDbContext context)
        {
            _context = context;

        }
        public async Task<int> Create(CategoryCreateRequest request)
        {
            {
                var category = new Category()
                {
                    Name = request.Name,
                    Description = request.Description                   
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category.Id;
            }
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new Exception($"Cannot find a category: {categoryId}");
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryVm>> GetAll()
        {
            var categories = await _context.Categories.OrderByDescending(category => category.Id).Select(category => new CategoryVm()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            }).ToListAsync();
            return categories;
        }

        public async Task<CategoryVm> GetById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            var categoryViewModel = new CategoryVm()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description, 
            };
            return categoryViewModel;
        }

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null) throw new Exception($"Cannot find a category with id: {request.Id}");
            category.Name = request.Name;
            category.Description = request.Description;
            return await _context.SaveChangesAsync();

        }
    }
}
