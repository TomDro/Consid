using Consid.Domain.Models;
using Consid.Infrastucture.AzureTableStorage.Extensions;
using FluentAssertions;
using System;
using Xunit;

namespace Consid.Infrastucture.AzureTableStorage.Tests.Extensions
{
	public class LogExtensionsTests
	{
		[Fact]
		public void MapToEntity_MustBeValidLog()
		{
			var dateTime = new DateTimeOffset(2023, 06, 01, 22, 20, 55, TimeSpan.Zero);
			var logModel = new Log("someId", dateTime, 200, "somePayloadAddress");

			var logEntity = logModel.MapToEntity();

			logEntity.Should()
				.NotBeNull();

			logEntity.Timestamp.Should()
				.BeNull();

			logEntity.ETag.ToString().Should()
				.Be(string.Empty);

			logEntity.LogDateTime.Should()
				.Be(logModel.LogDateTime);

			logEntity.HttpStatus.Should()
				.Be(logModel.HttpStatusCode);

			logEntity.PartitionKey.Should()
				.Be("20230601");

			logEntity.RowKey.Should()
				.Be("20230601222055");

			logEntity.PayloadAddress.Should()
				.Be(logModel.PayloadAddress);
		}
	}
}
