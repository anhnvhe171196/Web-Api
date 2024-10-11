using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;
		[HttpGet]
		[Route("get-all-category")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var repone = new ApiResponse
				{
					success = true,
					message = "List Of Category",
					data = await _categoryRepository.GetAllCategoryAsync()
				};
				return Ok(repone);
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get all category fail"
				});
			}
		}
		[HttpPost]
		[Route("add-category")]
		public async Task<IActionResult> AddCategory([FromForm] CategoryDTO model)
		{
			try
			{
				var repone = new ApiResponse
				{
					success = true,
					message = "Add Success",
					data = await _categoryRepository.AddCategoryAsync(model)
				};
				return StatusCode(StatusCodes.Status201Created, repone);
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Created category fail"
				});
			}
		}
		[HttpGet("get-category-by-name/{name}")]
		public async Task<IActionResult> GetCategoryByName(string name)
		{
			try
			{
				var repone = new ApiResponse
				{
					success = true,
					message = $"List of category have {name}",
					data = await _categoryRepository.GetCategoryByNameAsync(name)
				};
				return Ok(repone);
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get category by name fail"
				});
			}
		}
		[HttpGet("get-category-by-id/{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			try
			{
				var repone = new ApiResponse
				{
					success = true,
					message = $"Category have {id}",
					data = await _categoryRepository.GetCategoryByIDAsync(id)
				};
				return Ok(repone);
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Get category by id fail"
				});
			}
		}
		[HttpPut("update-category-by-id/{id}")]
		public async Task<IActionResult> UpdateCategory( int id, [FromForm] CategoryDTO model)
		{
			try
			{
				await _categoryRepository.UpdateCategory(model, id);
				var repone = new ApiResponse
				{
					success = true,
					message = $"Update success category have {id}",
				};

				return Ok(repone);
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Update category by id fail"
				});
			}
		}
	}
}
