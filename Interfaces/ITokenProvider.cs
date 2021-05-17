using System.Net;
using System.Threading.Tasks;
using RestServiceProviderServiceProvider.Models;

namespace RestServiceProviderServiceProvider.Interfaces
{
	public interface ITokenProvider
	{
		Task<TokenModel> GetTokenAsync();
		Task<TokenModel> GenerateTokenAsync(NetworkCredential credential);
	}
}