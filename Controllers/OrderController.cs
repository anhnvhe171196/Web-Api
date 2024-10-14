using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "CustomerPolicy")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _order;

		public OrderController(IOrderRepository managerAction) => _order = managerAction;


		[HttpPost("buy")]
		public async Task<IActionResult> AddOrder([FromForm] OrderDTO order)
		{
			var cart = HttpContext.Session.GetString("Cart");
			List<AddProductToCartDTO> cartList = JsonConvert.DeserializeObject<List<AddProductToCartDTO>>(cart);
			if(cartList.Count <1)
			{
				return Ok(new ApiResponse
				{
					success = false,
					message = "Giở hàng hiện giờ chưa có sản phẩm nào"
				});

			}
			await _order.AddAsync(order, cartList);
			return Ok(new ApiResponse
			{
				success = true,
				message = "Mua hàng thành công"
			});
		}
		[HttpDelete("remove-order")]
		public async Task<IActionResult> RemoveOrder(int IdOrder)
		{
			try
			{
				await _order.DeleteAsync(IdOrder);
				return Ok(new ApiResponse
				{
					success = true,
					message = "Hủy đơn hành thàng công"
				});
			}
			catch
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Hủy đơn hành thất bại"
				});
			}
		}
		[HttpGet("get-all-order")]
		public async Task<IActionResult> GetAllOrder()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Lịch sử giao dịch",
					data = await _order.GetAllAsync()
				});
			}
			catch
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "không thể lấy ra được lịch sử giao dịch của bạn"
				});
			}
		}
		[HttpGet("get-order-by-id{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Chi tiết order của {id}",
					data = await _order.GetByIdAsync(id)
				});
			}
			catch
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "không thể lấy ra được chi tiết giao dịch của bạn"
				});
			}
		}
		[HttpPost("cancel-order{id}")]
		public async Task<IActionResult> AddOrder(int id)
		{
			try
			{
				await _order.CancelOrder(id);
				return Ok(new ApiResponse
				{
					success = true,
					message = "Đã hủy đơn hàng"
				});
			}
			catch
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Hủy đơn hàng thất bại"
				});
			}
		}
	}
}
