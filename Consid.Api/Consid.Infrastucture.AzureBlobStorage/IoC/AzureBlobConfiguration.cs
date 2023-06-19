namespace Consid.Infrastucture.AzureBlobStorage.IoC
{
	public record AzureBlobConfiguration
	{
		public AzureBlobConfiguration(string connection, string containerName)
		{
			ContainerName = containerName;
			Connection = connection;
		}

		public string ContainerName { get; private set; }
		public string Connection { get; private set; }
	}
}
