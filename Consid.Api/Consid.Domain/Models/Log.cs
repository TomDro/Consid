namespace Consid.Domain.Models
{
	public record Log
	{
		public Log(string logId, DateTimeOffset logDateTime, int httpStatusCode, string payloadAddress)
		{
			LogId = logId;
			LogDateTime = logDateTime;
			HttpStatusCode = httpStatusCode;
			PayloadAddress = payloadAddress;
		}

		public string LogId { get; private set; }
		public DateTimeOffset LogDateTime { get; private set; }
		public int HttpStatusCode { get; private set; }
		public string PayloadAddress { get; private set; }
	}
}