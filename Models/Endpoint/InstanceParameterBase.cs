using System;
using System.IO;
using System.Text.Encodings.Web;
using RestServiceProviderServiceProvider.Endpoint.Entities;
using RestServiceProviderServiceProvider.Endpoint.Enums;

namespace RestServiceProviderServiceProvider.Models.Endpoint
{
	internal static class OutputParameterFactory
	{
		static FileInfo fileInfo = new FileInfo("");
		static bool IsShai() => !fileInfo.Name.EndsWith("js");

		public static IInstanceParameter Retrieve<T>(OperationParameter parameter, T value)
		{
			switch (parameter.Location)
			{
				case ParameterLocation.Path:
					return InstanceParameterBase<T>.Path(parameter.Name, value);
				case ParameterLocation.Body:
					return InstanceParameterBase<T>.Body(parameter.Name, value);
				case ParameterLocation.Query:
					return InstanceParameterBase<T>.Querystring(parameter.Name, value);
				case ParameterLocation.None:
				default:
					throw new ArgumentException(
						$"{parameter.Location} is not supported.",
						$"{nameof(parameter)}.{nameof(OperationParameter.Location)}");
			}
		}
	}

	internal interface IInstanceParameter
	{
		string Name { get; }
		ParameterLocation Location { get; }
		string Value { get; }
	}

	internal abstract class InstanceParameterBase<T> : IInstanceParameter
	{
		protected static string _formatDateTime = "YYYY-MM-DD";

		public string Name { get; private set; }
		public abstract ParameterLocation Location { get; }
		protected T ValueParameter { get; private set; }
		public abstract string Value { get; }

		protected InstanceParameterBase(string name, T value)
		{
			Name = name;
			ValueParameter = value;
		}

		protected InstanceParameterBase(T value)
			: this(string.Empty, value) { }

		public static IInstanceParameter Path(string name, T value) =>
			new ParameterByPath(name, value);

		public static IInstanceParameter Querystring(string name, T value) =>
			new ParameterByQuerystring(name, value);

		public static IInstanceParameter Body(string name, T value) =>
			new ParameterByContent(name, value);

		protected string Encode(T toEncode) =>
			(toEncode.GetType().Equals(typeof(DateTime))) ?
				Encode(DateTime.Parse(toEncode.ToString()).ToString(_formatDateTime))
			: Encode(toEncode.ToString());

		protected string Encode(string toEncode) =>
			UrlEncoder.Default.Encode(toEncode);

		private class ParameterByPath : InstanceParameterBase<T>
		{
			public ParameterByPath(string name, T value) : base(name, value) { }

			public override ParameterLocation Location => ParameterLocation.Path;

			public override string Value =>
				Encode(ValueParameter);
		}

		private class ParameterByContent : InstanceParameterBase<T>
		{
			public ParameterByContent(string name, T value) : base(name, value) { }

			public override ParameterLocation Location => ParameterLocation.Body;

			public override string Value =>
				Encode(ValueParameter);
		}

		private class ParameterByQuerystring : InstanceParameterBase<T>
		{
			public ParameterByQuerystring(string name, T value) : base(name, value) { }

			public override ParameterLocation Location => ParameterLocation.Query;

			public override string Value =>
				$"{Encode(Name)}={Encode(ValueParameter)}";
		}
	}
}