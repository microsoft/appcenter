using System.Net.Http;
using Acquaint.Abstractions;

namespace Acquaint.Common.Droid
{
	public class HttpClientHandlerFactory : IHttpClientHandlerFactory
	{
		public HttpClientHandler GetHttpClientHandler()
		{
			// not needed on Android
			return null;
		}
	}
}

