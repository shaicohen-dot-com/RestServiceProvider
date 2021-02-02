namespace VDT.Common.ServiceProvider.Endpoint.Entities
{
	public class ParameterValue
	{
		public string Name { get; set; }
		public object Value { get; set; }

		public ParameterValue() { }
		public ParameterValue(string name, object value)
			: this()
		{
			Name = name;
			Value = value;
		}
	}



}