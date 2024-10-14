using ProjectApi.DTOs;
using ProjectApi.Models;

namespace ProjectApi.Repositoris
{
	public interface IManagerActionRepository
	{
		public Task<ApiResponse> EnterInvoice(InvoiceDTO invoiceDTO);
		public Task<ApiResponse> RemoveInvoiveById(int id);
		public Task<ApiResponse> GetAllInvoice();
		public Task<ApiResponse> GetInvoiceById(int id);
		public Task<ApiResponse> ChangeStatusOrder(int id, StatusOrderDTO model);
		public Task<List<OrderDetailsDTO>> GetAllAsync();
	}
}
