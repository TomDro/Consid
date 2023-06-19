namespace Consid.WebApi.Dtos
{
	public record LogQueryParameters
	{
		public LogQueryParameters(string from, string to)
		{
			From = from;
			To = to;
		}

		public string From { get; private set; }
		public string To { get; private set; }
	}
}
