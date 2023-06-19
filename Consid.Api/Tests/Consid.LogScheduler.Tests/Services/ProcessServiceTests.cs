using Consid.Domain.Models;
using Consid.Domain.Services.Abstractions;
using Consid.LogScheduler.Dtos;
using Consid.LogScheduler.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Consid.LogScheduler.Tests.Services
{
	public class ProcessServiceTests
	{
		private readonly ProcessService _processService;
		private readonly Mock<ILogWriteRepository> _logRepositoryMock = new();
		private readonly Mock<IPayloadWriteRepository> _payloadRepositoryMock = new();

		public ProcessServiceTests()
		{
			_processService = new(_logRepositoryMock.Object, _payloadRepositoryMock.Object);
		}

		// todo:
		// StartProcessAsync must be refactored cuz of hard verify whether proper message data was saved
		// Savings must be in transaction in case of failue --> try catch
		[Fact]
		public async Task StartProcessAsync_MustSaveLogAndPayload()
		{
			var dataToProcess = new ApiResponseDto(200, "some content");

			await _processService.StartProcessAsync(dataToProcess);

			_payloadRepositoryMock.Verify(x => x.SaveAsync(It.Is<Payload>(p => p.Content == "some content")), Times.Exactly(1));

			_logRepositoryMock.Verify(x => x.SaveLogAsync(It.Is<Log>(l => l.HttpStatusCode == 200)), Times.Exactly(1));
		}
	}
}
