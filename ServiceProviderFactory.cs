using System;
using System.Net;
using VDT.Common.ServiceProvider.ApiSpeciification;
using VDT.Common.ServiceProvider.Interfaces;

namespace VDT.Common.ServiceProvider
{
	public static class ServiceProviderFactory
	{
		public static IVdtServiceProvider StartProvider(Uri uriApi, IAuthenticationManager authenticationManager, string apiJsonDefinitionFilePath) =>
			new InternalServiceProvider(
				uriApi: uriApi,
				specificationProvider: new JsonSpecificationProvider(apiJsonDefinitionFilePath),
				authenticationManager: authenticationManager);
		//{

		//	provider = new JwtEnabledProvider(
		//		uriApi: uriApi,
		//		uriAuth: uriAuth,
		//		clientCredentials: clientCredentials,
		//		specificationProvider: new JsonSpecificationProvider(apiJsonDefinitionFilePath),
		//		tokenPath: tokenPath
		//		);
		//}
	}

}