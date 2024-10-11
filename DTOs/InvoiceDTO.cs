using ProjectApi.Helper.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.DTOs
{
	public class InvoiceDTO
	{
		[Required]
		public string Supplier { get; set; } = null!;
		[Required]
		[CheckDate]
		public DateTime ReceiptDate { get; set; }
		[Required]
		[CheckProductToImport]
		public List<ImportProductDTO> Products { get; set; } = null!;
	}
}
