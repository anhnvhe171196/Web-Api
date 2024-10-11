using APIWeb.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;

namespace ProjectApi.Services
{
	public class AdminActionService : IAdminActionRepository
	{
		private readonly MyOnlineShopContext _context;
		private readonly IMapper _mapper;
		public AdminActionService(MyOnlineShopContext context, IMapper mapper, IOptionsMonitor<AppSetting> optionsMonitor)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<List<CustomerInfoDTO>> GetAllCustomer()
		{
			var lists = await _context.Customers.ToListAsync();
			return _mapper.Map<List<CustomerInfoDTO>>(lists);
		}

		public async Task<List<ManagerInfoDTO>> GetAllManager()
		{
			var lists = await _context.Managers.ToListAsync();
			return _mapper.Map<List<ManagerInfoDTO>>(lists);
		}

		public async Task<List<UserInfoDTO>> GetAllUser()
		{
			var lists = await _context.Users.ToListAsync();
			return _mapper.Map<List<UserInfoDTO>>(lists);
		}

		public async Task<CustomerInfoDTO> GetCustomerByName(string email)
		{
			return _mapper.Map<CustomerInfoDTO>(await _context.Customers.SingleOrDefaultAsync(p => p.Email == email));
		}

		public async Task<ManagerInfoDTO> GetManagerByName(string email)
		{
			return _mapper.Map<ManagerInfoDTO>(await _context.Managers.SingleOrDefaultAsync(p => p.Email == email));
		}

		public async Task<UserInfoDTO> GetUserByName(string email)
		{
			return _mapper.Map<UserInfoDTO>(await _context.Users.SingleOrDefaultAsync(p => p.Email == email));
		}

		public async Task<ApiResponse> SetRole(SetRoleDTO model)
		{
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
			user.RoleName = model.Role.ToString();
			user.Activated = model.Active;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Sửa thông tin thành công",
			};
		}

		public async Task UpdateManager(string email, ManagerUpdateDTO model)
		{
			var manager = await _context.Managers.SingleOrDefaultAsync(m =>  m.Email == email);
			manager.Activated = model.Activated;
			manager.IsSeniorManager = model.IsSeniorManager;
			manager.Salary = model.Salary;
			_context.Managers.Update(manager);
			await _context.SaveChangesAsync();
		}
	}
}
