using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class ProductInfoDTO
	{
		public string ProductName {  get; set; }
		public decimal ProductPrice { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string ProductDescription { get; set; }
		public string ProductCategory { get; set; }
		public int ProductQuantity { get; set; }
		public string[] ProductImage { get; set; }
	}
}
