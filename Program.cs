using APIWeb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectApi.Repositoris;
using ProjectApi.Services;
using ProjectWebApi.Data;
using System.Text;

namespace ProjectApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddAutoMapper(typeof(Program));

			builder.Services.AddScoped<ICategoryRepository, CategoryService>();
			builder.Services.AddScoped<IAccountRepository, AccountService>();
			builder.Services.AddScoped<IProductRepository, ProductService>();
			builder.Services.AddScoped<IAdminActionRepository, AdminActionService>();
			builder.Services.AddScoped<IManagerActionRepository, ManagerActionService>();
			builder.Services.AddScoped<IUserActionRepository, UserActionService>();
			builder.Services.AddScoped<IOrderRepository, OrderService>();
			builder.Services.AddHttpContextAccessor();

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminPolicy", policy =>
				{
					policy.RequireRole("Admin");
				});
				options.AddPolicy("ManagerPolicy", policy =>
				{
					policy.RequireRole("Manager");
				});
				options.AddPolicy("CustomerPolicy", policy =>
				{
					policy.RequireRole("Customer");
				});
			});
			
			// Configure Swagger
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web Api Demo", Version = "v1" });

				// Configure the security scheme
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				});

				// Add security requirement
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});

			// Configure database context
			builder.Services.AddDbContext<MyOnlineShopContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
			});
			builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));
			var secretKey = builder.Configuration["AppSettings:SecretKey"];
			var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					//Tự cấp token
					ValidateIssuer = false,
					ValidateAudience = false,

					//Ký vào token
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
					ClockSkew = TimeSpan.Zero,

				};
			});
			builder.Services.AddSwaggerGenNewtonsoftSupport();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API V1");
				});
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSession();
			app.MapControllers();
			app.Run();
		}
	}
}
