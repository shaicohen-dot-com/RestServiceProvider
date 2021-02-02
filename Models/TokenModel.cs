//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace VDT.Common.ServiceProvider.Models
{
	public class TokenModel
	{
		public static TokenModel Empty() => new TokenModel();

		[JsonProperty("access_token")]
		public string Token { get; set; }
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
		[JsonProperty("expires_in")]
		public uint ExpiresIn { get; set; }
		[JsonProperty("token_type")]
		public string TokenType { get; set; }
	}
}