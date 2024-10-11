using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectWebApi.Data;
using System.Drawing;

namespace ProjectApi.Repositoris
{
	public interface IAccountRepository
	{
		public Task<ApiResponse> RegisterUser(RegitsterUserDTO model);
		public Task<ApiResponse> RegisterCutormer(RegitsterUserDTO model, string? Adress);
		public Task<ApiResponse> RegisterManager(RegitsterUserDTO model);
		public Task<ApiResponse> UserLogin(UserLogin model);
		public Task<ApiResponse> Logout();
		public Task<ApiResponse> RenewToken(Token token);
		public Task<ApiResponse> EnableTwoFactor();
		public Task<ApiResponse> DisableTwoFactor(string otp);
		public Task<ApiResponse> VerifyTwoFactorCode(string email, string otpCode);
	}
}
