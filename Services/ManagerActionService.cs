using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;
using System.Security.Claims;

namespace ProjectApi.Services
{
	public class ManagerActionService : IManagerActionRepository
	{
		private readonly MyOnlineShopContext _context;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ManagerActionService(MyOnlineShopContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<ApiResponse> EnterInvoice(InvoiceDTO invoiceDTO)
		{
			var userId = GetUserOfToken();
			var invoice = _mapper.Map<Invoice>(invoiceDTO);
			invoice.ManagerId = Guid.Parse(userId);
			invoice.TotalAmount = invoiceDTO.Products.Sum(p => (p.Price * p.Quantity));

			await _context.Invoices.AddAsync(invoice);
			await _context.SaveChangesAsync();

			var listProduct = _mapper.Map<List<ImportProduct>>(invoiceDTO.Products);
            foreach (var item in listProduct)
            {
				var product = await _context.ProductDetails.SingleOrDefaultAsync(p => p.ProductId == item.ProductId);
				item.InvoiceId = invoice.Id;
				product.Quantity += item.Quantity;
				_context.ProductDetails.Update(product);
			}

			await _context.ImportProducts.AddRangeAsync(listProduct);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Tạo Invoice thành công",
			};
		}

		public async Task<ApiResponse> GetAllInvoice()
		{
			var userId = GetUserOfToken();
			var invoice = await _context.Invoices.Include(i => i.Manager)
				.Where(i => i.ManagerId == Guid.Parse(userId))
				.Select(i => new
				{
					InvoiceId = i.Id,
					ManagerCode = i.Manager.EmployeeCode,
					ReceiptDate = i.ReceiptDate,
					SupplierName = i.Supplier,
					ListProduct = i.ImportProducts
				}).ToListAsync();
			return new ApiResponse
			{
				success = true,
				message = "Danh sách Invoice",
				data = invoice,
			};
		}

		public async Task<ApiResponse> GetInvoiceById(int id)
		{
			var userId = GetUserOfToken();
			var invoice = await _context.Invoices.Include(i => i.Manager)
				.Where(i => i.ManagerId == Guid.Parse(userId) && i.Id == id)
				.Select(i => new
				{
					InvoiceId = i.Id,
					ManagerCode = i.Manager.EmployeeCode,
					ReceiptDate = i.ReceiptDate,
					SupplierName = i.Supplier,
					ListProduct = i.ImportProducts
				}).SingleOrDefaultAsync();
			return new ApiResponse
			{
				success = true,
				message = $"Lấy ra Invoice có id: {id} thành công",
				data = invoice,
			};
		}

		public async Task<ApiResponse> RemoveInvoiveById(int id)
		{
			var userId = GetUserOfToken();
			var invoice = await _context.Invoices.Include(i => i.Manager)
				.Where(i => i.ManagerId == Guid.Parse(userId) && i.Id == id)
				.SingleOrDefaultAsync();
			_context.Invoices.Remove(invoice);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = $"đã xóa Invoice có id: {id} thành công",
			};
		}

		public string GetUserOfToken()
		{
			var user = _httpContextAccessor.HttpContext.User;
			return user.FindFirst("Id")?.Value;
		}

		public async Task<ApiResponse> ChangeStatusOrder(int id, StatusOrderDTO model)
		{
			var order = await _context.OrderDetails.SingleOrDefaultAsync(o => o.OrderId == id);
			if(order == null)
			{
				return new ApiResponse
				{
					success = true,
					message = "Không tồn tại Order này"
				};
			}
			if(order.Status == "DaHuy" || order.Status == "DaGiaoHang")
			{
				return new ApiResponse
				{
					success = true,
					message = "Order này hiện đã hoàn thành hoặc đã bị hủy"
				};
			}
			order.RequireDate = model.RequireDate;
			order.Status = model.OrderStatus.ToString();
			_context.OrderDetails.Update(order);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Bạn đã thay đổi trạng thái thành công"
			};
		}

		public async Task<List<OrderDetailsDTO>> GetAllAsync()
		{
			var order = await _context.OrderDetails.Include(p => p.Order).ThenInclude(p => p.OrderProducts).ToListAsync();
			List<OrderDetailsDTO> orderdetails = new List<OrderDetailsDTO>();
			foreach (var item in order)
			{
				var orderdetail = _mapper.Map<OrderDetailsDTO>(item);
				orderdetail.OrderProducts = _mapper.Map<List<OrderProductDTO>>(item.Order.OrderProducts);
				orderdetails.Add(orderdetail);
			}
			return orderdetails;
		}
	}
}
