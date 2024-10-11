using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectWebApi.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBanHang.Helpers
{
    public class MyUtil
    {
		private static Random random = new Random();
		
		public static string GenerateRamdomKey(int length = 5)
        {
            var pattern = @"qazwsxedcrfvtgbyhnujmiklopQAZWSXEDCRFVTGBYHNUJMIKLOP!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }
		public static async Task<string> GetFileName(IFormFile logo)
		{
			if (logo.Length > 0)
			{
				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				var fileExtension = Path.GetExtension(logo.FileName);
				var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
				var path = Path.Combine(folderPath, uniqueFileName);

				using (var stream = System.IO.File.Create(path))
				{
					await logo.CopyToAsync(stream);
				}

				return "/images/" + uniqueFileName;
			}

			return null;
		}

		public static string GenerateUniqueEmployeeCode(MyOnlineShopContext _context)
		{
			string employeeCode;

			do
			{
				employeeCode = GenerateRandomCode();
			}
			while (EmployeeCodeExists(employeeCode, _context));

			return employeeCode;
		}

		public static string GenerateRandomCode()
		{
			const string chars = "0123456789";
			string randomPart = new string(Enumerable.Repeat(chars, 4)
				.Select(s => s[random.Next(s.Length)]).ToArray());

			return "MA" + randomPart;
		}

		public static bool EmployeeCodeExists(string employeeCode, MyOnlineShopContext _context)
		{
			return _context.Managers.Any(e => e.EmployeeCode == employeeCode);
		}
		public static async Task<string> GetFileName(IFormFile[] logos)
		{
			if (logos.Length > 0)
			{
				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}
				string result = "";
                foreach (var logo in logos)
                {
					var fileExtension = Path.GetExtension(logo.FileName);
					var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
					var path = Path.Combine(folderPath, uniqueFileName);

					using (var stream = System.IO.File.Create(path))
					{
						await logo.CopyToAsync(stream);
					}
					result += "/images/" + uniqueFileName + ",";
				}


				return result.TrimEnd(',');
			}

			return string.Empty;
		}
	}
}
