using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "AdminPolicy")]
	public class AdminActionController : ControllerBase
	{
		private readonly IAdminActionRepository _admin;

		public AdminActionController(IAdminActionRepository admin) => _admin = admin;

		

		[HttpGet("get-all-user")]
		public async Task<IActionResult> GetAllUser()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "List all user",
					data = await _admin.GetAllUser()
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "không thể lấy tất cả user",
				});
			}
		}
		[HttpGet("get-all-customer")]
		public async Task<IActionResult> GetAllCustomer()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "List all user",
					data = await _admin.GetAllCustomer()
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "không thể lấy tất cả cutomer",
				});
			}
		}
		[HttpGet("get-all-manager")]
		public async Task<IActionResult> GetAllManager()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "List all user",
					data = await _admin.GetAllManager()
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "không thể lấy tất cả manager",
				});
			}
		}
		[HttpGet("get-user-by-email/{email}")]
		public async Task<IActionResult> GetUserByEmail(string email)
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Lấy ra thông tin của user có email là: {email}",
					data = await _admin.GetUserByName(email)
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Không tìm thấy",
				});
			}
		}
		[HttpGet("get-customer-by-email/{email}")]
		public async Task<IActionResult> GetCutomerByEmail(string email)
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Lấy ra thông tin của Cutomer có email là: {email}",
					data = await _admin.GetCustomerByName(email)
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Không tìm thấy",
				});
			}
		}
		[HttpGet("get-manager-by-email/{email}")]
		public async Task<IActionResult> GetManagerByEmail(string email)
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Lấy ra thông tin của manager có email là: {email}",
					data = await _admin.GetManagerByName(email)
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Không tìm thấy",
				});
			}
		}
		[HttpPut("set-role-user")]
		public async Task<IActionResult> SetRole([FromForm] SetRoleDTO model)
		{
			try
			{
				return Ok(await _admin.SetRole(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Sửa lại thông tin thất bại",
				});
			}
		}
		[HttpPut("update-manager/{email}")]
		public async Task<IActionResult> UpdateManage(string email,[FromForm] ManagerUpdateDTO model)
		{
			try
			{
				await _admin.UpdateManager(email, model);
				return Ok(new ApiResponse
				{
					success = true,
					message = "Cập nhập manager thành công",
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Cập nhập manager thất bại",
				});
			}
		}
	}
}
