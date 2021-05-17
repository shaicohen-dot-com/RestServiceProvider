using System.Text.Json.Serialization;

namespace RestServiceProviderServiceProvider.Models
{
	public class TokenModel
	{
		public static TokenModel Empty() => new TokenModel();

		[JsonPropertyName("access_token")]
		public string Token { get; set; }
		[JsonPropertyName("refresh_token")]
		public string RefreshToken { get; set; }
		[JsonPropertyName("expires_in")]
		public uint ExpiresIn { get; set; }
		[JsonPropertyName("token_type")]
		public string TokenType { get; set; }
	}
}