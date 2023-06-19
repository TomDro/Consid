using Consid.LogScheduler.Dtos;

namespace Consid.LogScheduler.Services
{
	public interface IProcessService
	{
		Task StartProcessAsync(ApiResponseDto data);
	}
}
