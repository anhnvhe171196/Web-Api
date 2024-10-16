﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Repositoris;

namespace ProjectApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Policy = "ManagerPolicy")]
	public class ManagerActionController : ControllerBase
	{
		private readonly IManagerActionRepository _managerAction;

		public ManagerActionController(IManagerActionRepository managerAction) => _managerAction = managerAction;

		[HttpPost("add-invoice")]
		public async Task<IActionResult> AddInvoice([FromBody] InvoiceDTO model)
		{
			try
			{
				return StatusCode(StatusCodes.Status201Created, await _managerAction.EnterInvoice(model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Tạo invoice thất bại"
				});
			}
		}
		[HttpGet("get-all-invoice")]
		public async Task<IActionResult> GetAllInvoice()
		{
			try
			{
				return Ok( await _managerAction.GetAllInvoice());
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Không lấy được invoice"
				});
			}
		}
		[HttpGet("get-invoice-by-id/{id}")]
		public async Task<IActionResult> GetInvoiceById(int id)
		{
			try
			{
				return Ok(await _managerAction.GetInvoiceById(id));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Không lấy được invoice"
				});
			}
		}
		[HttpPost("update-status-oder-by-id/{id}")]
		public async Task<IActionResult> UpdateOrder(int id, [FromForm] StatusOrderDTO model) 
		{
			try
			{
				return Ok(await _managerAction.ChangeStatusOrder(id, model));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Chưa update được trạng thái"
				});
			}
		}
		[HttpDelete("remove-invoice/{id}")]
		public async Task<IActionResult> DeleteInvoice(int id)
		{
			try
			{
				return Ok(await _managerAction.RemoveInvoiveById(id));
			}
			catch
			{
				return BadRequest(new ApiResponse
				{
					success = false,
					message = "Không xóa được invoice"
				});
			}
		}
		[HttpGet("get-all-order")]
		public async Task<IActionResult> GetAllOrder()
		{
			try
			{
				return Ok(new ApiResponse
				{
					success = true,
					message = "Lịch sử giao dịch",
					data = await _managerAction.GetAllAsync()
				});
			}
			catch
			{
				return Ok(new ApiResponse
				{
					success = false,
					message = "không thể lấy ra được lịch sử giao dịch"
				});
			}
		}
	}
}
