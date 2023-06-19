namespace Consid.Infrastucture.AzureTableStorage.IoC
{
	public record AzureTableConfiguration
	{
		public AzureTableConfiguration(string connection, string tableName)
		{
			TableName = tableName;
			Connection = connection;
		}

		public string TableName { get; private set; }
		public string Connection { get; private set; }
	}
}
