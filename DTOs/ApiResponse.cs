using System.Text.Json.Serialization;

namespace ProjectApi.Models
{
	public class ApiResponse
	{
		public bool success {  get; set; }
		public string message { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Object data { get; set; }
	}
}
