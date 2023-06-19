using Consid.LogScheduler.Configuration;
using Consid.LogScheduler.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Consid.LogScheduler.Tests.Services
{
	public class DataApiGatewayTests
	{
		private readonly DataApiGateway _dataApiGateway;
		private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
		private readonly Mock<IOptions<DataSourceOptions>> _dataSourceOptionsMock = new();

		public DataApiGatewayTests()
		{
			_dataSourceOptionsMock.SetupGet(x => x.Value).Returns(new DataSourceOptions { DataSourceUrl = "http://someUrl" });
			_dataApiGateway = new(_httpClientFactoryMock.Object, _dataSourceOptionsMock.Object);
		}

		[Theory]
		[InlineData(200, "{content: test data}")]
		[InlineData(400, "Some error")]
		[InlineData(401, "Not authorized")]
		[InlineData(500, "Internal error")]
		public async Task GetDataAsync_ForApiResponse_MustReturnSameHttpStatusAndContent(int httpCode, string data)
		{
			var httpClientMock = new MockHttpMessageHandler();

			httpClientMock.When("http://someUrl")
				.Respond((HttpStatusCode)httpCode, new StringContent(data));

			var client = httpClientMock.ToHttpClient();

			_httpClientFactoryMock.Setup(_ => _.CreateClient(string.Empty))
				.Returns(client);

			var result = await _dataApiGateway.GetDataAsync();

			result.HttpStatus.Should()
				.Be(httpCode);
			result.Content.Should()
				.Be(data);
		}
	}
}
