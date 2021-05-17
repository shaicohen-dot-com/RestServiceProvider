using System.Runtime.Serialization;

namespace RestServiceProviderServiceProvider.Endpoint.Enums
{
	[DataContract]
	public enum ParameterValueType
	{
		[EnumMember(Value = "null")]
		None = 0,
		[EnumMember(Value = "boolean")]
		Boolean = 1,
		[EnumMember(Value = "string")]
		String = 2,
		[EnumMember(Value = "datetime")]
		DateTime = 3,
		[EnumMember(Value = "array")]
		Array = 4
	}



}