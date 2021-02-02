using System.Net;
using System.Threading.Tasks;
using VDT.Common.ServiceProvider.Models;

namespace VDT.Common.ServiceProvider.Interfaces
{
	public interface ITokenProvider
	{
		Task<TokenModel> GetTokenAsync();
		Task<TokenModel> GenerateTokenAsync(NetworkCredential credential);
	}
}