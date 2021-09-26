using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestServiceProviderServiceProvider.Endpoint;
using RestServiceProviderServiceProvider.Endpoint.Entities;
using RestServiceProviderServiceProvider.Endpoint.Enums;

namespace RestServiceProviderServiceProvider.Parser
{
	/// <summary>
	/// converts a swagger api document into an EndPointManager
	/// <para>this parser was hacked together for a specific set of api calls. 
	/// should probably be replaced with an openapi spec compliant parser
	/// </para>
	/// </summary>
	internal static class SwaggerSpecificationParser
	{

		public static ApiDocument Parse(string apiDefinition)
		{
			ApiDocument apiDoc = JsonSerializer.Deserialize<ApiDocument>(apiDefinition);
			return apiDoc;
		}

		public static EndpointManager ToManager(this ApiDocument apiDocument)
		{

			return new EndpointManager
			{
				Endpoints = getEndpoints(apiDocument.Paths.Select(p => (p.Key, p.Value)))
			};

			IEnumerable<ApiResourceEndpoint> getEndpoints(IEnumerable<(string path, Operation operationMethods)> operations) =>
				operations.Select(op =>
				new ApiResourceEndpoint
				{
					Path = op.path,
					Operations = getOperations(op.operationMethods.Methods)
				});

			IEnumerable<EndpointOperation> getOperations(IEnumerable<Parser.Action> actions) =>
					actions.Select(a =>
						new EndpointOperation
						{
							Name = a.OperationId,
							Method = a.Method,
							Parameters = getParameters(a.Parameters),
						});

			IEnumerable<OperationParameter> getParameters(IEnumerable<Parameter> parameters) =>
				parameters is null || parameters.Count() < 1 ?
					new List<OperationParameter>()
					: parameters
						.Where(p => p != null)
						.Select(p =>
							new OperationParameter
							{
								Name = p.Name,
								Type = (ParameterValueType)(Enum.Parse(typeof(ParameterValueType), p.Type, true)),
								IsRequired = p.Required,
								Location = p.In
							});
		}
	}

	#region classes for parsing api spec

	/// <summary>
	/// the api document
	/// top level entity
	/// </summary>
	public class ApiDocument
	{
		[JsonPropertyName("info")]
		public Info Info { get; set; }
		[JsonPropertyName("paths")]
		public Dictionary<string, Operation> Paths { get; set; }
	}
	public class Info
	{
		[JsonPropertyName("version")]
		public string Version { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }
	}

	public class Schema
	{
		[JsonPropertyName("$ref")]
		public string Ref { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("items")]
		public Items Items { get; set; }
	}

	public class Operation
	{
		[JsonPropertyName("get")]
		public Get Get { get; set; }
		[JsonPropertyName("post")]
		public Post Post { get; set; }
		public IEnumerable<Action> Methods { get => new List<Action> { Get, Post }.Where(a => a != null); }
	}

	public class Parameter
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("in")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ParameterLocation In { get; set; }

		[JsonPropertyName("required")]
		public bool Required { get; set; }

		[JsonPropertyName("schema")]
		public Schema Schema { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("format")]
		public string Format { get; set; }

		[JsonPropertyName("items")]
		public Items Items { get; set; }

		[JsonPropertyName("collectionFormat")]
		public string CollectionFormat { get; set; }
	}

	public class _200
	{
		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("schema")]
		public Schema Schema { get; set; }
	}

	public class Responses
	{
		[JsonPropertyName("200")]
		public _200 _200 { get; set; }
	}

	public class Action
	{
		public virtual OperationMethod Method { get; }

		[JsonPropertyName("tags")]
		public List<string> Tags { get; set; }

		[JsonPropertyName("operationId")]
		public string OperationId { get; set; }

		[JsonPropertyName("consumes")]
		public List<object> Consumes { get; set; }

		[JsonPropertyName("produces")]
		public List<string> Produces { get; set; }

		[JsonPropertyName("parameters")]
		public List<Parameter> Parameters { get; set; }

		[JsonPropertyName("responses")]
		public Responses Responses { get; set; }

	}
	public class Post : Action
	{
		public override OperationMethod Method => OperationMethod.POST;
	}

	public class Get : Action
	{
		public override OperationMethod Method => OperationMethod.GET;
	}

	public class ApiBroadcast
	{
		[JsonPropertyName("post")]
		public Post Post { get; set; }
	}

	public class Items
	{
		[JsonPropertyName("$ref")]
		public string Ref { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }
	}

	#endregion classes for parsing api spec
}

