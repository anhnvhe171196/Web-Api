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
	public class AccountController : ControllerBase
	{
		private readonly IAccountRepository _account;

		public AccountController(IAccountRepository account) => _account = account;
		[HttpPost("register-user")]
		public async Task<IActionResult> RegisterUser([FromForm] RegitsterUserDTO model)
		{
			try
			{
				return Ok(await _account.RegisterUser(model));
			}
			catch
			{
				return StatusCode(StatusCodes.Status409Conflict ,new ApiResponse
				{
					success = false,
					message = "Đăng kí thất bại",
				});
			}
		}
		[HttpPost("register-manager")]
		public async Task<IActionResult> RegisterManager([FromForm] RegitsterUserDTO model)
		{
			try
			{
				
				return StatusCode(StatusCodes.Status201Created, await _account.RegisterManager(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Đăng kí thất bại",
				});
			}
		}
		[HttpPost("register-customer")]
		public async Task<IActionResult> RegisterCustomer([FromForm] RegitsterUserDTO model, [FromForm] string? Adress)
		{
			try
			{
				return StatusCode(StatusCodes.Status201Created, await _account.RegisterManager(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Đăng kí thất bại",
				});
			}
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromForm] UserLogin model)
		{
			try
			{
				return StatusCode(StatusCodes.Status201Created, await _account.UserLogin(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Login thất bại",
				});
			}
		}
		[Authorize]
		[HttpPost("enableTwoFactor")]
		public async Task<IActionResult> EnableTwoFactor()
		{
			try
			{
				return Ok(await _account.EnableTwoFactor());
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Bật xác thực 2 lần thất bại",
				});
			}
		}
		[Authorize]
		[HttpPost("disableTwoFacter")]
		public async Task<IActionResult> DisableTwoFacter(string otp)
		{
			try
			{
				return Ok(await _account.DisableTwoFactor(otp));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Bật xác thực 2 lần thất bại",
				});
			}
		}
		[HttpPost("verifyTwoFactorCode")]
		public async Task<IActionResult> VerifyTwoFactorCode([FromForm] string email, string otp)
		{
			try
			{
				return Ok(await _account.VerifyTwoFactorCode(email, otp));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Xác thực thất bại",
				});
			}
		}
		[HttpPost("RenewToken")]
		public async Task<IActionResult> RenewToken([FromForm] Token model)
		{
			try
			{
				return StatusCode(StatusCodes.Status201Created,await _account.RenewToken(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "tạo mới token thất bại",
				});
			}
		}
		
	}
}
