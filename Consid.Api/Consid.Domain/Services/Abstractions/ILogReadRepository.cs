using Consid.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Consid.Domain.Services.Abstractions
{
	public interface ILogReadRepository
	{
		public Task<Log[]> GetLogsAsync(DateTimeOffset from, DateTimeOffset to);
	}
}
