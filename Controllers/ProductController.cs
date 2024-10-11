using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		IProductRepository _productRepository;
	    public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpPost("add-product")]
		public async Task<IActionResult> AddProduct(ProductDetailDTO model)
		{
			try
			{
				await _productRepository.AddProduct(model);
				return StatusCode(StatusCodes.Status201Created, new ApiResponse
				{
					success = true,
					message = "Create Product Success"
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Create Product fails"
				});
			}
		}
		[HttpGet("all-product")]
		[Authorize]

		public async Task<IActionResult> GetAllProduct()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Get All Products Success",
					data = await _productRepository.GetAllProducts()
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get All Products fails"
				});
			}
		}
		[HttpGet("all-product-latest")]
		public async Task<IActionResult> GetAllLatestProduct()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Get All Products Latest Success",
					data = await _productRepository.GetAllLatestProducts()
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get All Products Latest fails"
				});
			}
		}
		[HttpGet("all-product-special")]
		public async Task<IActionResult> GetAllSpecialProduct()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Get All Products Special Success",
					data = await _productRepository.GetAllSpecialProducts()
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get All Products Special fails"
				});
			}
		}
		[HttpGet("get-product-by-name{name}")]
		public async Task<IActionResult> GetProductByName(string name)
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Get All Products by {name}",
					data = await _productRepository.GetProductByName(name)
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get Products fails"
				});
			}
		}
		[HttpGet("get-product-by-id/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Get All Products by {id}",
					data = await _productRepository.GetProductById(id)
				});
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get Products fails"
				});
			}
		}
		[HttpPut("update-product-by-id/{id}")]
		public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO model, bool? available)
		{
			try
			{
				await _productRepository.UpdateProduct(id, model, available);
				return Ok(new ApiResponse
				{
					success = true,
					message = $"Update Products {id} success",
					
				});
			}
			catch(DbUpdateException ex)
			{
				Console.WriteLine(ex.InnerException?.Message);
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Update Products fails"
				});
			}
		}
	}
}
