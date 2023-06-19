using Consid.Domain.Models;
using System.Threading.Tasks;

namespace Consid.Domain.Services.Abstractions
{
	public interface IPayloadReadRepository
	{
		Task<Payload> GetAsync(string payloadId);
	}
}
