using Consid.LogScheduler.Dtos;
using System.Threading.Tasks;

namespace Consid.LogScheduler.Services
{
	public interface IProcessService
	{
		Task StartProcessAsync(ApiResponseDto data);
	}
}
