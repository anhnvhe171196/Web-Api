using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectWebApi.Data;

namespace ProjectApi.Repositoris
{
	public interface IOrderRepository
	{
		public Task<List<OrderDetailsDTO>> GetAllAsync();
		public Task<OrderDetailsDTO> GetByIdAsync(int orderId);
		public Task AddAsync(OrderDTO orderDetail, List<AddProductToCartDTO> listProduct);
		public Task DeleteAsync(int orderId);
		public Task CancelOrder(int orderId);
	}
}
