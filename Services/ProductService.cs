using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApi.DTOs;
using ProjectApi.Repositoris;
using ProjectWebApi.Data;
using ShopBanHang.Helpers;

namespace ProjectApi.Services
{
	public class ProductService : IProductRepository
	{
		private readonly MyOnlineShopContext _context;
		private readonly IMapper _mapper;

		public ProductService(MyOnlineShopContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task AddProduct(ProductDetailDTO model)
		{
			var product = new Product
			{
				CategoryId = await _context.Categories.Where(c => c.Name == model.CategoryName).Select(c => c.Id).FirstOrDefaultAsync(),
				Latest = model.Latest,
				Special = model.Special,
			};
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			var productDetail = _mapper.Map<ProductDetail>(model);
			productDetail.ProductId = product.Id;
			productDetail.Image = await MyUtil.GetFileName(model.Image);
			await _context.ProductDetails.AddAsync(productDetail);
			await _context.SaveChangesAsync();
		}

		public async Task<List<ProductInfoDTO>> GetAllLatestProducts()
		{
			var productDetails = await _context.ProductDetails.Include(p => p.Product).ThenInclude(p => p.Category).Where(p => p.Product.Latest == true && p.Product.Available == true).ToListAsync();
			List<ProductInfoDTO> products = new List<ProductInfoDTO>();

			foreach (var item in productDetails)
			{
				products.Add(_mapper.Map<ProductInfoDTO>(item));
			}
			return products;
		}

		public async Task<List<ProductInfoDTO>> GetAllProducts()
		{
			var productDetails = await _context.ProductDetails.Include(p => p.Product).ThenInclude(p => p.Category).Where(p => p.Product.Available == true).ToListAsync();
			List<ProductInfoDTO> products = new List<ProductInfoDTO>();

			foreach (var item in productDetails)
			{
				products.Add(_mapper.Map<ProductInfoDTO>(item));
			}
			return products;
		}

		public async Task<List<ProductInfoDTO>> GetAllSpecialProducts()
		{
			var productDetails = await _context.ProductDetails.Include(p => p.Product).ThenInclude(p => p.Category).Where(p => p.Product.Special == true && p.Product.Available == true).ToListAsync();
			List<ProductInfoDTO> products = new List<ProductInfoDTO>();

			foreach (var item in productDetails)
			{
				products.Add(_mapper.Map<ProductInfoDTO>(item));
			}
			return products;
		}

		public async Task<ProductInfoDTO> GetProductById(int id)
		{
			var productDetails = await _context.ProductDetails.Include(p => p.Product).ThenInclude(p => p.Category).SingleOrDefaultAsync(p => p.Product.Id == id);
			var productInfo = _mapper.Map<ProductInfoDTO>(productDetails);
			productInfo.ProductName = await _context.Categories.Where(c => c.Id == productDetails.Product.CategoryId).Select(c => c.Name).FirstOrDefaultAsync();
			return productInfo;
		}

		public async Task<List<ProductInfoDTO>> GetProductByName(string name)
		{
			var productDetails = await _context.ProductDetails.Include(p => p.Product).ThenInclude(p => p.Category).Where(p => p.Name.Contains(name) && p.Product.Available == true).ToListAsync();
			List<ProductInfoDTO> products = new List<ProductInfoDTO>();

			foreach (var item in productDetails)
			{
				products.Add(_mapper.Map<ProductInfoDTO>(item));
			}
			return products;
		}

		public async Task UpdateProduct(int id, UpdateProductDTO model, bool? available)
		{
			var productDetails = await _context.ProductDetails.Include(p => p.Product).SingleOrDefaultAsync(p => p.ProductId == id);

			var product = productDetails.Product;

			if (!string.IsNullOrEmpty(model.Name))
			{
				productDetails.Name = model.Name;
			}

			if (model.IntakePrice.HasValue && model.IntakePrice.Value > 0)
			{
				productDetails.IntakePrice = model.IntakePrice.Value;
			}

			if (model.SellPrice.HasValue && model.SellPrice.Value > productDetails.IntakePrice)
			{
				productDetails.SellPrice = model.SellPrice.Value;
			}

			if (model.Quantity.HasValue && model.Quantity.Value > 0)
			{
				productDetails.Quantity = model.Quantity.Value;
			}

			if (!string.IsNullOrEmpty(model.Description))
			{
				productDetails.Description = model.Description;
			}

			product.Available = available ?? product.Available;

			product.CategoryId = await _context.Categories
				.Where(c => c.Name == model.CategoryName)
				.Select(c => c.Id)
				.FirstOrDefaultAsync();


			if (model.Image != null && model.Image.Length > 0)
			{
				productDetails.Image = await MyUtil.GetFileName(model.Image);
			}

			if (model.Special.HasValue)
			{
				product.Special = model.Special.Value;
			}

			if (model.Latest.HasValue)
			{
				product.Latest = model.Latest.Value;
			}

			_context.Products.Update(product);
			_context.ProductDetails.Update(productDetails);
			await _context.SaveChangesAsync();
		}

	}
}
