using Consid.LogScheduler.Dtos;

namespace Consid.LogScheduler.Services
{
	public interface IDataApiGateway
	{
		public Task<ApiResponseDto> GetDataAsync();
	}
}
