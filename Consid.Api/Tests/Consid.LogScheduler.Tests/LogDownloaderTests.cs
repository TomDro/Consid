using Consid.LogScheduler.Dtos;
using Consid.LogScheduler.Services;
using Consid.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Consid.LogScheduler.Tests
{
	public class LogDownloaderTests
	{
		private readonly LogDownloader _logDownloader;
		private readonly Mock<ILogger<LogDownloader>> _loggerMock = new();
		private readonly Mock<IDataApiGateway> _dataApiGatewayMock = new();
		private readonly Mock<IProcessService> _processServiceMock = new();

		private readonly MyInfo _myInfo = new MyInfo() { ScheduleStatus = new() { Next = DateTime.UtcNow.AddMinutes(1) } };
		public LogDownloaderTests()
		{
			_logDownloader = new(_loggerMock.Object, _dataApiGatewayMock.Object, _processServiceMock.Object);
		}

		[Fact]
		public async Task Run_ForAnyDataReturnedByApi_MustProcessThatData()
		{
			var apiResponse = new ApiResponseDto(200, "some content");

			_dataApiGatewayMock.Setup(x => x.GetDataAsync())
				.ReturnsAsync(apiResponse);

			await _logDownloader.Run(_myInfo);

			_dataApiGatewayMock.Verify(x => x.GetDataAsync(), Times.Exactly(1));

			_processServiceMock.Verify(x => x.StartProcessAsync(apiResponse), Times.Exactly(1));
		}

		[Fact]
		public async Task Run_WhenApiThrowsException_MustNotProcessAndHandleThatException()
		{
			_dataApiGatewayMock.Setup(x => x.GetDataAsync())
				.ThrowsAsync(new Exception("some ex"));

			await FluentActions.Awaiting(() => _logDownloader.Run(_myInfo))
				.Should()
				.NotThrowAsync();

			_dataApiGatewayMock.Verify(x => x.GetDataAsync(), Times.Exactly(1));

			_processServiceMock.Verify(x => x.StartProcessAsync(It.IsAny<ApiResponseDto>()), Times.Never);
		}

		[Fact]
		public async Task Run_WhenProcessThrowsException_MustHandleThatException()
		{
			var apiResponse = new ApiResponseDto(200, "some content");

			_dataApiGatewayMock.Setup(x => x.GetDataAsync())
				.ReturnsAsync(apiResponse);

			_processServiceMock.Setup(x => x.StartProcessAsync(apiResponse))
				.ThrowsAsync(new Exception("some ex"));

			await FluentActions.Awaiting(() => _logDownloader.Run(_myInfo))
				.Should()
				.NotThrowAsync();

			_dataApiGatewayMock.Verify(x => x.GetDataAsync(), Times.Exactly(1));

			_processServiceMock.Verify(x => x.StartProcessAsync(apiResponse), Times.Exactly(1));
		}
	}
}
