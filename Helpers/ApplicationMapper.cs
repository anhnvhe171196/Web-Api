using AutoMapper;
using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectWebApi.Data;
namespace ProjectApi.Helper
{
	public class ApplicationMapper : Profile
	{
		public ApplicationMapper()
		{
			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<User, RegitsterUserDTO>().ReverseMap();
			CreateMap<Manager, RegitsterUserDTO>().ReverseMap();
			CreateMap<Customer, RegitsterUserDTO>().ReverseMap();
			
			CreateMap<User, UserInfoDTO>().ReverseMap();
			CreateMap<Manager, ManagerInfoDTO>().ReverseMap();
			CreateMap<Customer, CustomerInfoDTO>().ReverseMap();
			CreateMap<Invoice, InvoiceDTO>().ReverseMap();
			CreateMap<OrderDetail, OrderDTO>().ReverseMap();
			CreateMap<User, UserInfoDTO>().ReverseMap();
			CreateMap<Customer, UserInfoDTO>().ReverseMap();
			CreateMap<Invoice, InvoiceDTO>().ReverseMap();
			CreateMap<OrderDetail, OrderDetailsDTO>().ReverseMap();
			CreateMap<OrderProduct, OrderProductDTO>().ReverseMap();

			CreateMap<ImportProduct, ImportProductDTO>().ReverseMap();
			CreateMap<Product, ProductDetailDTO>()
			.ForMember(dest => dest.CategoryName, opt => opt.Ignore())
			.ReverseMap()
			.ForMember(dest => dest.Category, opt => opt.Ignore());
			CreateMap<ProductDetail, ProductDetailDTO>().ReverseMap();
			CreateMap<ProductDetail, ProductInfoDTO>()
			.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.SellPrice))
			.ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description))
			.ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.Product.Category.Name))
			.ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.Quantity))
			.ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Image.Split(",", StringSplitOptions.RemoveEmptyEntries)))
			.ReverseMap()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
			.ForMember(dest => dest.SellPrice, opt => opt.MapFrom(src => src.ProductPrice))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
			.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProductQuantity))
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => string.Join(",", src.ProductImage)));
		}
	}
}
