using System;
using System.Net;
using System.Threading.Tasks;
using VDT.Common.ServiceProvider.Interfaces;
using VDT.Common.ServiceProvider.Models;

namespace VDT.Common.ServiceProvider.Providers
{
	public class TestTokenProvider : ITokenProvider
	{
		public uint ExpiresInSeconds { get; set; }  = 60;

		public TestTokenProvider(uint expiresInSeconds = 60)
		{
			ExpiresInSeconds = expiresInSeconds;
		}

		public async Task<TokenModel> GetTokenAsync()
		{
			//Thread.Sleep(tokenRetrieveDelay);
			return await Task.FromResult(
					new TokenModel
					{
						Token = Guid.NewGuid().ToString("N"),
						TokenType = "TestToken",
						ExpiresIn = ExpiresInSeconds
					});
		}

		public Task<TokenModel> GenerateTokenAsync(NetworkCredential credential) =>
			GetTokenAsync();

	}

}