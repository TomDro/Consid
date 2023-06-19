using Azure.Storage.Blobs;
using Consid.Domain.Services.Abstractions;
using Consid.Infrastucture.AzureBlobStorage.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Infrastucture.AzureBlobStorage.IoC
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBlobStorage(this IServiceCollection serviceCollection, AzureBlobConfiguration configuration)
		{
			return serviceCollection.AddSingleton(provider =>
			{
				return new PayloadRepository(new BlobContainerClient(configuration.Connection, configuration.ContainerName));
			})
			.AddSingleton<IPayloadWriteRepository>(provider => provider.GetRequiredService<PayloadRepository>())
			.AddSingleton<IPayloadReadRepository>(provider => provider.GetRequiredService<PayloadRepository>());
		}
	}
}
