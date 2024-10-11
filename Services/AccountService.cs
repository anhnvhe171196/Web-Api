using APIWeb.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using ProjectApi.DTOs;
using ProjectApi.Helper;
using ProjectApi.Models;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;
using QRCoder;
using ShopBanHang.Helpers;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace ProjectApi.Services
{
	public class AccountService : IAccountRepository
	{
		private readonly MyOnlineShopContext _context;
		private readonly IMapper _mapper;
		private readonly AppSetting _appSettings;
		private static ConcurrentDictionary<string, string> TokenCache = new ConcurrentDictionary<string, string>();
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AccountService(MyOnlineShopContext context, IMapper mapper, IOptionsMonitor<AppSetting> optionsMonitor, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_mapper = mapper;
			_appSettings = optionsMonitor.CurrentValue;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<ApiResponse> RegisterCutormer(RegitsterUserDTO model, string? Adress)
		{

			var user = _mapper.Map<Customer>(model);
			user.RandomKey = MyUtil.GenerateRamdomKey();
			user.Password = model.Password.ToMd5Hash(user.RandomKey);
			user.Photo = "/images/anhtamthoi.jpg";
			user.RoleName = "Customer";
			user.Address = Adress;
			user.TwoFactorEnabled = false;
			await _context.Customers.AddAsync(user);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Đăng kí thành công",
			};
		}

		public async Task<ApiResponse> RegisterUser(RegitsterUserDTO model)
		{
			var user = _mapper.Map<User>(model);
			user.RandomKey = MyUtil.GenerateRamdomKey();
			user.Password = model.Password.ToMd5Hash(user.RandomKey);
			user.Photo = "/images/anhtamthoi.jpg";
			user.RoleName = "User";
			user.TwoFactorEnabled = false;
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Đăng kí thành công",
			};
		}

		public async Task<ApiResponse> RegisterManager(RegitsterUserDTO model)
		{
			var manager = _mapper.Map<Manager>(model);
			manager.RandomKey = MyUtil.GenerateRamdomKey();
			manager.Password = model.Password.ToMd5Hash(manager.RandomKey);
			manager.Photo = "/images/anhtamthoi.jpg";
			manager.EmployeeCode = MyUtil.GenerateUniqueEmployeeCode(_context);
			manager.RoleName = "Manager";
			manager.Activated = false;
			manager.TwoFactorEnabled = false;
			await _context.Managers.AddAsync(manager);
			await _context.SaveChangesAsync();
			return new ApiResponse
			{
				success = true,
				message = "Đăng kí thành công",
			};
		}

		public async Task<ApiResponse> UserLogin(UserLogin model)
		{
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
			if (user == null || !user.Activated)
			{
				return new ApiResponse
				{
					success = false,
					message = "Thông tin đăng nhập không hợp lệ"
				};
			}

			if (model.Password.ToMd5Hash(user.RandomKey) != user.Password)
			{
				return new ApiResponse
				{
					success = false,
					message = "Thông tin đăng nhập không hợp lệ"
				};
			}

			if (user.TwoFactorEnabled)
			{
				return new ApiResponse
				{
					success = true,
					message = "Nhập mã OTP từ ứng dụng xác thực"
				};
			}
			var token = await GenarateToken(user);
			return new ApiResponse
			{
				success = true,
				message = "Đăng nhập thành công",
				data = token
			};
		}


		public async Task<ApiResponse> RenewToken(Token model)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

			var tokenValidateParam = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

				ClockSkew = TimeSpan.Zero,
				ValidateLifetime = false
			};
			var tokenInverification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

			if (validatedToken is JwtSecurityToken jwtSecurityToken)
			{
				var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
				if (!result)
				{
					return new ApiResponse
					{
						success = false,
						message = "Access token không hợp lệ"
					};
				}
			}

			var utcExpireDate = long.Parse(tokenInverification.Claims.FirstOrDefault(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Exp).Value);
			var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);

			if (expireDate > DateTime.UtcNow)
			{
				return new ApiResponse
				{
					success = false,
					message = "Access token chưa hết hạn"
				};
			}

			var userIdClaim = tokenInverification.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
			if (string.IsNullOrEmpty(userIdClaim))
			{
				return new ApiResponse
				{
					success = false,
					message = "Không tìm thấy user"
				};
			}

			var user = await _context.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userIdClaim);
			if (user == null)
			{
				return new ApiResponse
				{
					success = false,
					message = "User không tồn tại"
				};
			}

			var newToken = await GenarateToken(user);
			return new ApiResponse
			{
				success = true,
				message = "Tạo mới token thành công",
				data = new Token
				{
					AccessToken = newToken.AccessToken,
					RefreshToken = newToken.RefreshToken
				}
			};
		}

		private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
		{
			var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
		}

		private string Generate2FASecret()
		{
			var key = KeyGeneration.GenerateRandomKey(20);
			return Base32Encoding.ToString(key);
		}

		private string GenerateQrCodeUri(string email, string secret)
		{
			var encodedEmail = Uri.EscapeDataString(email);
			return $"otpauth://totp/{encodedEmail}?secret={secret}&issuer=ProjectApi";
		}

		private Bitmap GenerateQrCode(string qrCodeUri)
		{
			var qrGenerator = new QRCodeGenerator();
			var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.Q);
			var qrCode = new QRCode(qrCodeData);
			return qrCode.GetGraphic(20);
		}

		private bool ValidateTwoFactorCode(User user, string otpCode)
		{
			var totp = new Totp(Base32Encoding.ToBytes(user.TwoFactorSecret));
			return totp.VerifyTotp(otpCode, out long timeWindowUsed);
		}

		public async Task<ApiResponse> VerifyTwoFactorCode(string email, string otpCode)
		{
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
			if (user == null)
			{
				return new ApiResponse
				{
					success = false,
					message = "Người dùng không tồn tại"
				};
			}

			if (ValidateTwoFactorCode(user, otpCode))
			{
				var token = await GenarateToken(user);
				return new ApiResponse
				{
					success = true,
					message = "Xác thực thành công",
					data = token
				};
			}

			return new ApiResponse
			{
				success = false,
				message = "Mã OTP không hợp lệ"
			};
		}

		public async Task<ApiResponse> EnableTwoFactor()
		{
			var usertemp = _httpContextAccessor.HttpContext.User;
			var userEmail = usertemp.FindFirst("Email")?.Value;
			var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail);
			if (user == null)
			{
				return new ApiResponse
				{
					success = false,
					message = "Người dùng không tồn tại"
				};
			}
			if(user.TwoFactorEnabled)
			{
				return new ApiResponse
				{
					success = false,
					message = "Bạn đã bật truy cập 2 lớp rồi",
				};
			}
			user.TwoFactorSecret = Generate2FASecret();
			user.TwoFactorEnabled = true;
			await _context.SaveChangesAsync();

			var qrCodeUri = GenerateQrCodeUri(userEmail, user.TwoFactorSecret);
			var qrCodeImage = GenerateQrCode(qrCodeUri);

			if (qrCodeImage == null)
			{
				return new ApiResponse
				{
					success = false,
					message = "Lỗi khi tạo mã QR"
				};
			}

			using (var stream = new MemoryStream())
			{
				qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
				var qrCodeBytes = stream.ToArray();

				if (qrCodeBytes.Length == 0)
				{
					return new ApiResponse
					{
						success = false,
						message = "Không có dữ liệu hình ảnh"
					};
				}

				var emailSent = await SendEmailWithQrCode(userEmail, qrCodeBytes);
				if (!emailSent)
				{
					return new ApiResponse
					{
						success = false,
						message = "Lỗi khi gửi email"
					};
				}

				return new ApiResponse
				{
					success = true,
					message = "Đã kích hoạt 2FA thành công"
				};
			}
		}

		private async Task<bool> SendEmailWithQrCode(string recipientEmail, byte[] qrCodeBytes)
		{
			try
			{
				var fromAddress = new MailAddress("anhnvhe171196@fpt.edu.vn", "ANHNVHE171196");
				var toAddress = new MailAddress(recipientEmail);
				const string subject = "2FA Activation - QR Code";

				// Simple email body without the base64 image
				string body = @"
					<h4>Scan the QR Code attached to activate 2FA:</h4>
					<p>If you cannot scan the QR code, manually enter the secret in your authenticator app.</p>";

				// Create the email message
				var message = new MailMessage
				{
					From = fromAddress,
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				};
				message.To.Add(toAddress);

				using (var stream = new MemoryStream(qrCodeBytes))
				{
					var attachment = new Attachment(stream, "qrcode.png", "image/png");
					message.Attachments.Add(attachment);

					using (var smtp = new SmtpClient
					{
						Host = "smtp.gmail.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential("anhnvhe171196@fpt.edu.vn", "sbqvzhfzavfjjchv")
					})
					{
						await smtp.SendMailAsync(message);
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private string GenerateToken(User user, int time)
		{
			var claims = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, user.FullName),
				new Claim("Id", user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.RoleName),
				new Claim("Email", user.Email),
			});

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claims,
				Expires = DateTime.UtcNow.AddDays(time),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		private async Task<Token> GenarateToken(User user)
		{
			var accessToken = GenerateToken(user, 1);
			var refreshToken = GenerateToken(user, 7);
			return new Token
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
			};
		}

		public Task<ApiResponse> Logout()
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResponse> DisableTwoFactor(string otp)
		{
			var usertemp = _httpContextAccessor.HttpContext.User;
			var userEmail = usertemp.FindFirst("Email")?.Value;
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == userEmail);
			if (!user.TwoFactorEnabled)
			{
				return new ApiResponse
				{
					success = false,
					message = "Bạn chưa bật chức năng xác thực 2 lớp"
				};
			}
			if(ValidateTwoFactorCode(user, otp))
			{
				user.TwoFactorEnabled = false;
				user.TwoFactorSecret = null;
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
				return new ApiResponse
				{
					success = true,
					message = "Bạn đã tắt chức năng xác thực 2 lớp"
				};
			}
			return new ApiResponse
			{
				success = false,
				message = "Vui lòng kiểm tra lại mã OTP"
			};
		}
	}
}
