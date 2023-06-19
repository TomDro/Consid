using Azure;
using Azure.Storage.Blobs;
using Consid.Domain.Exceptions;
using Consid.Domain.Models;
using Consid.Domain.Services.Abstractions;
using System.Text;

namespace Consid.Infrastucture.AzureBlobStorage.Repositories
{
	public class PayloadRepository : IPayloadWriteRepository, IPayloadReadRepository
	{
		private readonly BlobContainerClient _blobContainerClient;

		public PayloadRepository(BlobContainerClient blobContainerClient)
		{
			_blobContainerClient = blobContainerClient;
		}

		public async Task<Payload> GetAsync(string payloadId)
		{
			try
			{
				var blob = await _blobContainerClient
					.GetBlobClient(payloadId)
					.DownloadContentAsync();

				var blobContent = blob.Value.Content.ToArray();
				var payloadContent = Encoding.UTF8.GetString(blobContent);
				return new Payload(payloadId, payloadContent);
			}
			catch (RequestFailedException ex)
			{
				throw new ResourceNotFoundException(payloadId, ex);
			}
		}

		public async Task SaveAsync(Payload payload)
{
			var contentInBytes = Encoding.UTF8.GetBytes(payload.Content);
			var binaryData = BinaryData.FromBytes(contentInBytes);
			await _blobContainerClient.UploadBlobAsync(payload.PayloadId, binaryData);
		}
	}
}