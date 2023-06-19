using Consid.Domain.Models;

namespace Consid.Domain.Services.Abstractions
{
	public interface ILogReadRepository
	{
		public Task<Log[]> GetLogsAsync(DateTimeOffset from, DateTimeOffset to);
	}
}
