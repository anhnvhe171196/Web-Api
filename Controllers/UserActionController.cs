using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserActionController : ControllerBase
	{
		private readonly IUserActionRepository _user;
		public UserActionController(IUserActionRepository user)
		{
			_user = user;
		}

		[HttpPost("update-user")]
		public async Task<IActionResult> UpdateUser(UpdateUserProfileDTO model)
		{
			try
			{
				return Ok(await _user.UpdateUser(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Cập nhật user thất bại"
				});
			}
		}
		[HttpPost("update-customer")]
		public async Task<IActionResult> UpdateCustomer(UpdateUserProfileDTO model, string? adress)
		{
			try
			{
				return Ok(await _user.UpdateUser(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Cập nhật user thất bại"
				});
			}
		}
		[HttpPost("change-password")]
		public async Task<IActionResult> ChangePassword([FromForm]ChangePasswordDTO model)
		{
			try
			{
				return Ok(await _user.ChangePassword(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Thay đổi mật khẩu thất bại"
				});
			}
		}
	}
}
