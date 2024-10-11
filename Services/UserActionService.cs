using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;
using ShopBanHang.Helpers;

namespace ProjectApi.Services
{
	public class UserActionService : IUserActionRepository
	{
		private readonly MyOnlineShopContext _context;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserActionService(MyOnlineShopContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<ApiResponse> ChangePassword(ChangePasswordDTO model)
		{
			var usertemp = _httpContextAccessor.HttpContext.User;
			var userid = usertemp.FindFirst("Id")?.Value;
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userid));
			if(user.Password != model.OldPassword.ToMd5Hash(user.RandomKey))
			{
				return new ApiResponse
				{
					success = true,
					message = "Mật khẩu không trùng khớp với mật khẩu cũ"
				};
			}
			if(model.NewPassword != model.ConfirmPassword)
			{
				return new ApiResponse
				{
					success = true,
					message = "không trùng khớp với mật khẩu mới"
				};
			}
			user.Password = model.NewPassword.ToMd5Hash(user.RandomKey);
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Thay đổi mật khẩu thành công"
			};
		}

		public async Task<ApiResponse> UpdateCustomer(UpdateUserProfileDTO model, string? Adress)
		{
			var usertemp = _httpContextAccessor.HttpContext.User;
			var userid = usertemp.FindFirst("Id")?.Value;
			var user = await _context.Customers.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userid));
			if (!string.IsNullOrWhiteSpace(model.FullName))
			{
				user.FullName = model.FullName;
			}
			if (!string.IsNullOrWhiteSpace(model.Phone))
			{
				user.Phone = model.Phone;
			}
			if (model.Photo != null && model.Photo.Length > 0)
			{
				user.Photo = await MyUtil.GetFileName(model.Photo);
			}
			user.Address = Adress;
			_context.Customers.Update(user);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Đã cập nhập thông tin cá nhân thành công"
			};
		}

		public async Task<ApiResponse> UpdateUser(UpdateUserProfileDTO model)
		{
			var usertemp = _httpContextAccessor.HttpContext.User;
			var userid = usertemp.FindFirst("Id")?.Value;
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(userid));
			if(!string.IsNullOrWhiteSpace(model.FullName))
			{
				user.FullName = model.FullName;
			}
			if (!string.IsNullOrWhiteSpace(model.Phone))
			{
				user.Phone = model.Phone;
			}
			if (model.Photo != null && model.Photo.Length > 0)
			{
				user.Photo = await MyUtil.GetFileName(model.Photo);
			}
			_context.Users.Update(user);

			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Đã cập nhập thông tin cá nhân thành công"
			};
		}
	}
}
