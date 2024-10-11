using ProjectApi.DTOs;
using ProjectApi.Models;

namespace ProjectApi.Repositoris
{
	public interface IUserActionRepository
	{
		public Task<ApiResponse> UpdateUser(UpdateUserProfileDTO model);
		public Task<ApiResponse> UpdateCustomer(UpdateUserProfileDTO model, string? Adress);
		public Task<ApiResponse> ChangePassword(ChangePasswordDTO model);
	}
}
