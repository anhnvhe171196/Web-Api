using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class ManagerInfoDTO : UserInfoDTO
	{
		public string EmployeeCode { get; set; } = null!;
	
		public DateTime DateOfJoining { get; set; }
		
		public bool IsSeniorManager { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? Salary { get; set; }
	}
}
