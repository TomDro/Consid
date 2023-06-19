using Consid.LogScheduler.Configuration;
using Consid.LogScheduler.Dtos;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consid.LogScheduler.Services
{
	internal class DataApiGateway : IDataApiGateway
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly string _dataSource;

		public DataApiGateway(
			IHttpClientFactory httpClientFactory,
			IOptions<DataSourceOptions> dataSourceOptions)
		{
			_httpClientFactory = httpClientFactory;
			_dataSource = dataSourceOptions.Value.DataSourceUrl;
		}

		public async Task<ApiResponseDto> GetDataAsync()
		{
			var response = await _httpClientFactory.CreateClient().GetAsync(_dataSource);
			var result = await response.Content.ReadAsStringAsync();

			return new ApiResponseDto((int)response.StatusCode, result);
		}
	}
}
