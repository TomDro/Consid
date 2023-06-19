using Consid.Domain.Models;
using System.Threading.Tasks;

namespace Consid.Domain.Services.Abstractions
{
	public interface IPayloadWriteRepository
	{
		Task SaveAsync(Payload payload);
	}
}
