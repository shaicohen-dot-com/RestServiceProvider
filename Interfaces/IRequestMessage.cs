using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VDT.Utilities;

namespace VDT.Common.ServiceProvider.Interfaces
{
	internal interface IRequestMessage
	{
		HttpRequestMessage ToRequest();
	}

	internal abstract class RequestMessageBase : IRequestMessage
	{
		protected abstract HttpMethod Method { get; }
		protected virtual HttpContent MessageContent { get; } = null;
		protected Uri Path { get; private set; } = null;
		protected AuthenticationHeaderValue AuthenticationHeader { get; private set; } = null;
		protected RequestMessageBase(Uri path)
		: this(path, null) { }
		protected RequestMessageBase(Uri path, AuthenticationHeaderValue authentication = null)
		{
			Path = path;
			AuthenticationHeader = authentication;
		}
		//protected RequestMessageBase(string path, AuthenticationHeaderValue authenticationHeader)
		//{
		//	Path = path;
		//	AuthenticationHeader = authenticationHeader;
		//}
		public HttpRequestMessage ToRequest()
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.Method = Method;
			request.RequestUri = Path;
			if (AuthenticationHeader != null)
				request.Headers.Authorization = AuthenticationHeader;
			if (MessageContent != null)
				request.Content = MessageContent;

			return request;
		}
		//internal AuthenticationHeaderValue AuthenticationHeader() =>
		//	new AuthenticationHeaderValue(
		//			"Bearer",
		//			Configuration.t
		//			;



	}
	internal class GetRequestMessage : RequestMessageBase
	{
		protected override HttpMethod Method => HttpMethod.Get;

		internal GetRequestMessage(Uri path, AuthenticationHeaderValue authenticationHeader = null)
			: base(path, authenticationHeader) { }

	}

	internal class PostRequestMessage<T> : RequestMessageBase where T : class
	{
		protected override HttpMethod Method => HttpMethod.Post;
		private T Payload { get; set; } = default;
		protected override HttpContent MessageContent =>
			new StringContent(
				Payload.Serialize(),
				System.Text.Encoding.UTF8,
				"application/json");

		internal PostRequestMessage(Uri path, T payload, AuthenticationHeaderValue authenticationHeader = null)
			: base(path, authenticationHeader)
		{
			Payload = payload;
		}

	}
	internal class FormUrlEncodedRequestMessage : RequestMessageBase
	{
		private IEnumerable<KeyValuePair<string, string>> Payload { get; set; }

		protected override HttpMethod Method => HttpMethod.Post;

		protected override HttpContent MessageContent =>
			new FormUrlEncodedContent(
				Payload);

		internal FormUrlEncodedRequestMessage(Uri path, IEnumerable<KeyValuePair<string, string>> payload, AuthenticationHeaderValue authenticationHeader = null)
		: base(path, authenticationHeader)
		{
			Payload = payload;
		}

	}
}
