using ProjectApi.DTOs;
using ProjectApi.Models;

namespace ProjectApi.Repositoris
{
	public interface IAdminActionRepository
	{
		public Task<List<UserInfoDTO>> GetAllUser();
		public Task<List<CustomerInfoDTO>> GetAllCustomer();
		public Task<List<ManagerInfoDTO>> GetAllManager();
		public Task<UserInfoDTO> GetUserByName(string email);
		public Task<CustomerInfoDTO> GetCustomerByName(string email);
		public Task<ManagerInfoDTO> GetManagerByName(string email);
		public Task UpdateManager(string email, ManagerUpdateDTO managerInfoDTO);
		public Task<ApiResponse> SetRole(SetRoleDTO model);

	}
}
