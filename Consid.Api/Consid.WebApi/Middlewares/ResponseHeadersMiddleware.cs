using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Consid.WebApi.Middlewares
{
	internal sealed class ResponseHeadersMiddleware : IFunctionsWorkerMiddleware
	{
		public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
		{
			await next(context);

			var responseData = context.GetHttpResponseData();

			responseData.Headers.Add("Content-Type", "text/plain; charset=utf-8");
		}
	}
}
