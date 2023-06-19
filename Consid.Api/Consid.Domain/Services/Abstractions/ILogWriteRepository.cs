using Consid.Domain.Models;

namespace Consid.Domain.Services.Abstractions
{
	public interface ILogWriteRepository
	{
		public Task SaveLogAsync(Log log);
	}
}
