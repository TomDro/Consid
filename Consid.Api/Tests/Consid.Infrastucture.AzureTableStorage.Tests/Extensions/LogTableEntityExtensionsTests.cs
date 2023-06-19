using Consid.Infrastucture.AzureTableStorage.Entities;
using Consid.Infrastucture.AzureTableStorage.Extensions;
using FluentAssertions;
using System;
using Xunit;

namespace Consid.Infrastucture.AzureTableStorage.Tests.Extensions
{
	public class LogTableEntityExtensionsTests
	{
		[Fact]
		public void MapToEntity_MustBeValidLog()
		{
			var dateTime = new DateTimeOffset(2023, 06, 01, 22, 20, 55, TimeSpan.Zero);
			var logEntity = new LogTableEntity(dateTime, 200, "somePayloadAddress");

			var logModel = logEntity.MapToModel();

			logModel.Should()
				.NotBeNull();

			logModel.LogDateTime.Should()
				.Be(logEntity.LogDateTime);

			logModel.HttpStatusCode.Should()
				.Be(logEntity.HttpStatus);

			logModel.LogId.Should()
				.Be(logEntity.RowKey);

			logModel.PayloadAddress.Should()
				.Be(logEntity.PayloadAddress);
		}
	}
}
