using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace ProjectApi.DTOs
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OrderStatus
	{
		[EnumMember(Value = "ChoXuLy")]
		ChoXuLy = 1,
		[EnumMember(Value = "DangXuLy")]
		DangXuLy = 2,
		[EnumMember(Value = "DaVanChuyen")]
		DaVanChuyen = 3,
		[EnumMember(Value = "DaGiaoHang")]
		DaGiaoHang = 4,
		[EnumMember(Value = "DaHuy")]
		DaHuy = 5            
	}
}
