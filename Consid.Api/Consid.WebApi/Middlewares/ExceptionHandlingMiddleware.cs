using Consid.WebApi.Services.Factories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace Consid.WebApi.Middlewares
{
	internal sealed class ExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
	{
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);

				var response = ExceptionResponseFactory.CreateHttpResponseData(await context.GetHttpRequestDataAsync(), ex);

				context.GetInvocationResult().Value = response;
			}
		}
	}
}
