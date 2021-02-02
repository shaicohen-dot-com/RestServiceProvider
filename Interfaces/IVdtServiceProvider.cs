using System.Collections.Generic;
using System.Threading.Tasks;
using VDT.Common.ServiceProvider.Endpoint.Entities;

namespace VDT.Common.ServiceProvider.Interfaces
{
	public interface IVdtServiceProvider
	{
		Task<T> RunAsync<T>(string operationName, IEnumerable<ParameterValue> parameterValues = null);
	}

}