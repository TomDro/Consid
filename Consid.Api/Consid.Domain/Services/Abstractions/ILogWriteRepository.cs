using Consid.Domain.Models;
using System.Threading.Tasks;

namespace Consid.Domain.Services.Abstractions
{
	public interface ILogWriteRepository
	{
		public Task SaveLogAsync(Log log);
	}
}
