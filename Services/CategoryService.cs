using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApi.DTOs;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;

namespace ProjectApi.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly MyOnlineShopContext _context;
        private readonly IMapper _mapper;

        public CategoryService(MyOnlineShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddCategoryAsync(CategoryDTO category)
        {
            var _category = _mapper.Map<Category>(category);
            await _context.Categories.AddAsync(_category);
            await _context.SaveChangesAsync();
            return _category.Id;
        }

        public async Task<List<CategoryDTO>> GetAllCategoryAsync()
        {
            return _mapper.Map<List<CategoryDTO>>(await _context.Categories.ToListAsync());
        }

        public async Task<CategoryDTO> GetCategoryByIDAsync(int id)
        {
            return _mapper.Map<CategoryDTO>(await _context.Categories.SingleOrDefaultAsync(c => c.Id == id));
        }

        public async Task<List<CategoryDTO>> GetCategoryByNameAsync(string name)
        {
            var lists = await _context.Categories.Where(c => c.Name.Contains(name)).ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(lists);
        }

        public async Task UpdateCategory(CategoryDTO model, int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.Name = model.Name;
                await _context.SaveChangesAsync();
            }
        }
    }
}
