using System.Net;
using System.Text.Json;
using Consid.Domain.Services.Abstractions;
using Consid.WebApi.Dtos;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Consid.WebApi.Endpoints
{
	public class LogEndpoint
	{
		private readonly ILogger<LogEndpoint> _logger;
		private readonly ILogReadRepository _logReadRepository;
		private readonly IValidator<LogQueryParameters> _validator;

		public LogEndpoint(ILogger<LogEndpoint> logger, ILogReadRepository logReadRepository, IValidator<LogQueryParameters> validator)
		{
			_logger = logger;
			_logReadRepository = logReadRepository;
			_validator = validator;
		}

		[Function("LogEndpoint")]
		public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "logs")] HttpRequestData req, 
			string from, string to)
		{
			_logger.LogInformation("C# HTTP trigger function processed a request.");

			_validator.ValidateAndThrow(new LogQueryParameters(from, to));

			var logs = await _logReadRepository.GetLogsAsync(DateTimeOffset.Parse(from), DateTimeOffset.Parse(to));

			var response = req.CreateResponse(HttpStatusCode.OK);

			response.WriteString(JsonSerializer.Serialize(logs));

			return response;
		}
	}
}
