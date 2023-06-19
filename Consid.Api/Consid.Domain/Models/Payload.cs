namespace Consid.Domain.Models
{
	public record Payload
	{
		public Payload(string payloadId, string content)
		{
			PayloadId = payloadId;
			Content = content;
		}

		public string PayloadId { get; private set; }
		public string Content { get; private set; }
	}
}
