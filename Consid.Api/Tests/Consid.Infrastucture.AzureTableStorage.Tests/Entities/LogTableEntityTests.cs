using Azure;
using Consid.Infrastucture.AzureTableStorage.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace Consid.Infrastucture.AzureTableStorage.Tests.Entities
{
	public class LogTableEntityTests
	{
		[Fact]
		public void LogTableEntityConstructor_MustBeValid()
		{
			var dateTimeOffset = new DateTimeOffset(2023, 06, 01, 21, 15, 55, TimeSpan.Zero);
			var logEntity = new LogTableEntity(dateTimeOffset, 204, "somePayloadAddress");

			logEntity.PartitionKey.Should()
				.Be("20230601");

			logEntity.RowKey.Should()
				.Be("20230601211555");

			logEntity.Timestamp.Should()
				.BeNull();

			logEntity.ETag.Should()
				.Be(new ETag());

			logEntity.HttpStatus.Should()
				.Be(204);

			logEntity.PayloadAddress.Should()
				.Be("somePayloadAddress");

			logEntity.LogDateTime.Should()
				.Be(dateTimeOffset);
		}
	}
}
