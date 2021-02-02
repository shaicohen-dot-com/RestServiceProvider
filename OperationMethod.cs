using System.Runtime.Serialization;

namespace VDT.Common.ServiceProvider
{
	[DataContract]
	public enum OperationMethod
	{
		[EnumMember(Value = "Not Set")]
		None = 0,
		[EnumMember(Value = "GET")]
		GET = 1,
		[EnumMember(Value = "POST")]
		POST = 2
	}



}