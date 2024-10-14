using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;
using System.Collections.Generic;

namespace ProjectApi.Services
{
	public class OrderService : IOrderRepository
	{
		private readonly MyOnlineShopContext _context;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public OrderService(MyOnlineShopContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task AddAsync(OrderDTO model, List<AddProductToCartDTO> listProduct)
		{
			var order = new Order();
			var userid = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
			order.CustomerId = Guid.Parse(userid);
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			var orderdetail = _mapper.Map<OrderDetail>(model);
			orderdetail.OrderId = order.Id;
			orderdetail.Status = OrderStatus.ChoXuLy.ToString();
			List<OrderProduct> orderProducts = new List<OrderProduct>();
			foreach (var item in listProduct)
			{
				var orderProduct = new OrderProduct();
				var product = _context.ProductDetails.SingleOrDefault(p => p.Name == item.ProductName);
				orderProduct.OrderId = order.Id;
				orderProduct.ProductId = product.ProductId;
				orderProduct.Quantity = item.Quantity;
				orderProduct.Price = product.SellPrice;
				orderProducts.Add(orderProduct);
			}
			await _context.OrderProducts.AddRangeAsync(orderProducts);
			orderdetail.Amount = orderProducts.Sum(p => p.Quantity * p.Price);
			if (!string.IsNullOrWhiteSpace(model.Discount))
			{
				if (Enum.TryParse<Discount>(model.Discount.ToUpper(), out var discountCode))
				{
					decimal discount = (int)discountCode;
					orderdetail.Amount -= orderdetail.Amount * (discount / 100);
				}
			}
			_context.OrderDetails.Add(orderdetail);
			await _context.SaveChangesAsync();
		}

		public async Task CancelOrder(int orderId)
		{
			var userid = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
			var order = await _context.OrderDetails.SingleOrDefaultAsync(o => o.OrderId == orderId && o.Order.CustomerId == Guid.Parse(userid));
			order.Status = "DaHuy";
			_context.OrderDetails.Update(order);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int orderId)
		{
			var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
		}

		public async Task<List<OrderDetailsDTO>> GetAllAsync()
		{
			var userid = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
			var order = await _context.OrderDetails.Include(p => p.Order).ThenInclude(p => p.OrderProducts).Where(o => o.Order.CustomerId == Guid.Parse(userid)).ToListAsync();
			List<OrderDetailsDTO> orderdetails = new List<OrderDetailsDTO>();
			foreach (var item in order)
			{
				var orderdetail = _mapper.Map<OrderDetailsDTO>(item);
				orderdetail.OrderProducts = _mapper.Map<List<OrderProductDTO>>(item.Order.OrderProducts);
				orderdetails.Add(orderdetail);
			}
			return orderdetails;
		}

		public async Task<OrderDetailsDTO> GetByIdAsync(int orderId)
		{
			var userid = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
			var order = await _context.OrderDetails.Include(p => p.Order).ThenInclude(p => p.OrderProducts).SingleOrDefaultAsync(o => (o.Order.CustomerId == Guid.Parse(userid) && o.OrderId == orderId));
			var orderdetail = _mapper.Map<OrderDetailsDTO>(order);
			orderdetail.OrderProducts = _mapper.Map<List<OrderProductDTO>>(order.Order.OrderProducts);
			return orderdetail;
		}
	}
}
