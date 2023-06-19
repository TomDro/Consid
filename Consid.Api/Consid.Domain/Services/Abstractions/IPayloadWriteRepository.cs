using Consid.Domain.Models;

namespace Consid.Domain.Services.Abstractions
{
	public interface IPayloadWriteRepository
	{
		Task SaveAsync(Payload payload);
	}
}
