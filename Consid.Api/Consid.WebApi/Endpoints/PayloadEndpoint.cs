using System.Net;
using Consid.Domain.Services.Abstractions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Consid.WebApi.Endpoints
{
	public class PayloadEndpoint
	{
		private readonly ILogger _logger;
		private readonly IPayloadReadRepository _payloadReadRepository;

		public PayloadEndpoint(ILoggerFactory loggerFactory, IPayloadReadRepository payloadReadRepository)
		{
			_logger = loggerFactory.CreateLogger<PayloadEndpoint>();
			_payloadReadRepository = payloadReadRepository;
		}

		[Function("PayloadEndpoint")]
		public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "payload/{payloadId}")] HttpRequestData req, string payloadId)
		{
			_logger.LogInformation("C# HTTP trigger function processed a request.");

			var payload = await _payloadReadRepository.GetAsync(payloadId);

			var response = req.CreateResponse(HttpStatusCode.OK);

			response.WriteString(payload.Content);

			return response;
		}
	}
}
