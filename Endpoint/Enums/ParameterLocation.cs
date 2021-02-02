using System.Runtime.Serialization;

namespace VDT.Common.ServiceProvider.Endpoint.Enums
{
	[DataContract]
	public enum ParameterLocation
	{
		[EnumMember(Value = "Not Set")]
		None = 0,
		[EnumMember(Value = "Path")]
		Path = 1,
		[EnumMember(Value = "Body")]
		Body = 2,
		[EnumMember(Value = "Query")]
		Query = 3
	}



}