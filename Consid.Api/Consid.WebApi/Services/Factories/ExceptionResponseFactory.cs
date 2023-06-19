using Consid.Domain.Exceptions;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Consid.WebApi.Services.Factories
{
	internal static class ExceptionResponseFactory
	{
		public static HttpResponseData CreateHttpResponseData(HttpRequestData requestData, Exception exception)
		{
			Exception ex = exception is AggregateException aggregate ? aggregate.InnerExceptions[0] : exception;

			var (statusCode, message) = ExtractStatusCodeAndMessage(ex);

			var response = requestData.CreateResponse();
			response.StatusCode = statusCode;
			
			using (var stream = GetBodyStream(message))
			{
				stream.CopyTo(response.Body);
			}

			return response;
		}

		private static (HttpStatusCode statusCode, string message) ExtractStatusCodeAndMessage(Exception ex)
		{
			switch (ex)
			{
				case ValidationException ve:
					return (HttpStatusCode.BadRequest, String.Join(Environment.NewLine, ve.Errors));
				case ResourceNotFoundException rnfe:
					return (HttpStatusCode.NotFound, rnfe.Message);
				default:
					return (HttpStatusCode.InternalServerError, "Internal Server Error");
			}
		}

		private static Stream GetBodyStream(string message)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(message));
		}
	}
}
