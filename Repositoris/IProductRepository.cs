using ProjectApi.DTOs;

namespace ProjectApi.Repositoris
{
	public interface IProductRepository
	{
		public Task AddProduct(ProductDetailDTO model);
		public Task<List<ProductInfoDTO>> GetAllProducts();
		public Task<List<ProductInfoDTO>> GetProductByName(string name);
		public Task<ProductInfoDTO> GetProductById(int id);
		public Task UpdateProduct(int id, UpdateProductDTO model, bool? available);
		public Task<List<ProductInfoDTO>> GetAllSpecialProducts();
		public Task<List<ProductInfoDTO>> GetAllLatestProducts();
	}

}