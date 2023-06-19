using Azure;
using Azure.Storage.Blobs;
using Consid.Domain.Exceptions;
using Consid.Infrastucture.AzureBlobStorage.Repositories;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Consid.Infrastucture.AzureBlobStorage.Tests.Repositories
{
	public class PayloadRepositoryTests
	{
		private readonly PayloadRepository _payloadRepository;
		private readonly Mock<BlobContainerClient> _blobContainerClientMock;

		public PayloadRepositoryTests()
		{
			_blobContainerClientMock = new();
			_payloadRepository = new(_blobContainerClientMock.Object);
		}

		[Fact]
		public async Task GetAsyncTest_WhenBlobNotFound_MustThrowNotFoundException()
		{
			var notExistingPayloadId = "payloadId";

			_blobContainerClientMock.Setup(x => x.GetBlobClient(notExistingPayloadId))
				.Throws(new RequestFailedException(""));

			await FluentActions.Awaiting(() => _payloadRepository.GetAsync(notExistingPayloadId))
				.Should()
				.ThrowExactlyAsync<ResourceNotFoundException>()
				.WithMessage("Resource payloadId not found");
		}

		[Fact(Skip = "Hard to test, method has to be refactored")]
		public void GetAsyncTest_WhenBlobIsFound_MustReturnPayload()
		{
			// todo: GetAsync() method should be refactored cuz of problem with test it
		}
	}
}