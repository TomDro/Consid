using Consid.LogScheduler.Dtos;
using System.Threading.Tasks;

namespace Consid.LogScheduler.Services
{
	public interface IDataApiGateway
	{
		public Task<ApiResponseDto> GetDataAsync();
	}
}
