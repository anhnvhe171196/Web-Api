using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectApi.DTOs;
using ProjectApi.Models;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "CustomerPolicy")]
	public class CartController : ControllerBase
	{
		[HttpGet("list-product-in-cart")]
		public IActionResult GetCart()
		{
			var cart = HttpContext.Session.GetString("Cart");
			if (cart == null)
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Hiện tại giỏ hàng của có sản phẩn nào"
				});
			}
			return Ok(JsonConvert.DeserializeObject<List<AddProductToCartDTO>>(cart));
		}

		[HttpPost]
		public IActionResult AddToCart([FromForm] AddProductToCartDTO product)
		{
			var cart = HttpContext.Session.GetString("Cart");
			List<AddProductToCartDTO> cartList = cart == null ? new List<AddProductToCartDTO>() : JsonConvert.DeserializeObject<List<AddProductToCartDTO>>(cart);
			var exitingProduct = cartList.FirstOrDefault(p => p.ProductName == product.ProductName);
			if(exitingProduct != null)
			{
				exitingProduct.Quantity += product.Quantity;
			}
			else
			{
				cartList.Add(product);
			}
			HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartList));

			return Ok(new ApiResponse
			{
				success = true,
				message = "Đã thêm sản phẩm vào giỏ hàng"
			});
		}

		[HttpDelete("remove-cart")]
		public IActionResult DeleteCart()
		{
			HttpContext.Session.Remove("Cart");
			return Ok(new ApiResponse
			{
				success = true,
				message = "Đã xóa giỏ hàng thành công"
			});
		}
		[HttpDelete("remove-product-in-cart")]
		public IActionResult DeleteProductInCart(string productName)
		{
			var cart = HttpContext.Session.GetString("Cart");
			List<AddProductToCartDTO> cartList = cart == null ? new List<AddProductToCartDTO>() : JsonConvert.DeserializeObject<List<AddProductToCartDTO>>(cart);
			var exitingProduct = cartList.FirstOrDefault(p => p.ProductName == productName);
			if( exitingProduct == null)
			{
				return Ok(new ApiResponse
				{
					success = false,
					message = "Sản phẩm không tồn tại trong giỏ hàng"
				});
			}
			cartList.Remove(exitingProduct);
			HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartList));

			return Ok(new ApiResponse
			{
				success = true,
				message = "Đã xóa sản phẩm khỏi giỏ hàng"
			});
		}
	}
}
