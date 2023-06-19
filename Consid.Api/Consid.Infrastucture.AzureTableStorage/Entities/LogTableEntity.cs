using Azure;
using Azure.Data.Tables;

namespace Consid.Infrastucture.AzureTableStorage.Entities
{
	internal class LogTableEntity : ITableEntity
	{
		public LogTableEntity()
		{

		}

		public LogTableEntity(DateTimeOffset dateTimeOffset, int httpStatus, string payloadAddress)
		{
			var date = DateOnly.FromDateTime(dateTimeOffset.DateTime);
			var time = TimeOnly.FromDateTime(dateTimeOffset.DateTime);

			PartitionKey = date.ToString("yyyyMMdd");
			RowKey = PartitionKey + time.ToString("HHmmss");
			HttpStatus = httpStatus;
			PayloadAddress = payloadAddress;
			LogDateTime = dateTimeOffset;
		}

		public string PartitionKey { get; set; }
		public string RowKey { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public ETag ETag { get; set; }
		public int HttpStatus { get; set; }
		public string PayloadAddress { get; set; }
		public DateTimeOffset LogDateTime { get; set; }
	}
}
