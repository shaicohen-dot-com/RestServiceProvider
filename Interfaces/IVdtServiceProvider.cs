using System.Collections.Generic;
using System.Threading.Tasks;
using RestServiceProviderServiceProvider.Endpoint.Entities;

namespace RestServiceProviderServiceProvider.Interfaces
{
	public interface IVdtServiceProvider
	{
		Task<T> RunAsync<T>(string operationName, IEnumerable<ParameterValue> parameterValues = null);
	}

}