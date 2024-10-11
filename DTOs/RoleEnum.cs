using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace ProjectApi.DTOs
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RoleEnum
	{
		[EnumMember(Value = "Admin")]
		Admin = 0,

		[EnumMember(Value = "Customer")]
		Customer = 1,

		[EnumMember(Value = "Manager")]
		Manager = 2,

		[EnumMember(Value = "User")]
		User = 3
	}


}
