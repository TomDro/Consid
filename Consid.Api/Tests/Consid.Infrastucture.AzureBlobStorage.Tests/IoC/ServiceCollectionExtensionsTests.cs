using Consid.Domain.Services.Abstractions;
using Consid.Infrastucture.AzureBlobStorage.IoC;
using Consid.Infrastucture.AzureBlobStorage.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Consid.Infrastucture.AzureBlobStorage.Tests.IoC
{
	public class ServiceCollectionExtensionsTests
	{
		[Fact]
		public void AddBlobStorageTests_WhenResolvingIPayloadWriteRepository_ShouldBeResolved()
		{
			var serviceCollection = new ServiceCollection();
			var blobConfiguration = new AzureBlobConfiguration("UseDevelopmentStorage=true", "test");

			serviceCollection.AddBlobStorage(blobConfiguration);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			var payloadWriteRepositoryImpl = serviceProvider.GetRequiredService<IPayloadWriteRepository>();

			payloadWriteRepositoryImpl.Should()
				.NotBeNull()
				.And.BeOfType<PayloadRepository>();
		}

		[Fact]
		public void AddBlobStorageTests_WhenResolvingIPayloadReadRepository_ShouldBeResolved()
		{
			var serviceCollection = new ServiceCollection();
			var blobConfiguration = new AzureBlobConfiguration("UseDevelopmentStorage=true", "test");

			serviceCollection.AddBlobStorage(blobConfiguration);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			var payloadWriteRepositoryImpl = serviceProvider.GetRequiredService<IPayloadReadRepository>();

			payloadWriteRepositoryImpl.Should()
				.NotBeNull()
				.And.BeOfType<PayloadRepository>();
		}
	}
}
