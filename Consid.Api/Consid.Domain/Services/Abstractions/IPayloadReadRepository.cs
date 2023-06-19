using Consid.Domain.Models;

namespace Consid.Domain.Services.Abstractions
{
	public interface IPayloadReadRepository
	{
		Task<Payload> GetAsync(string payloadId);
	}
}
