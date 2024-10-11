using ProjectApi.DTOs;

namespace ProjectApi.Repositoris
{
	public interface ICategoryRepository
	{
		public Task<List<CategoryDTO>> GetAllCategoryAsync();
		public Task<List<CategoryDTO>> GetCategoryByNameAsync(string name);
		public Task<CategoryDTO> GetCategoryByIDAsync(int id);
		public Task<int> AddCategoryAsync(CategoryDTO category);
		public Task UpdateCategory(CategoryDTO model, int id);

	}
}
