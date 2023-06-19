using Consid.Domain.Services.Abstractions;
using Consid.Infrastucture.AzureTableStorage.IoC;
using Consid.Infrastucture.AzureTableStorage.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Consid.Infrastucture.AzureTableStorage.Tests.IoC
{
	public class ServiceCollectionExtensionsTests
	{
		[Fact]
		public void AddAzureTableStorage_WhenResolvingILogReadRepository_ShouldBeResolved()
		{
			var serviceCollection = new ServiceCollection();
			var tableConfiguration = new AzureTableConfiguration("UseDevelopmentStorage=true", "test");

			serviceCollection.AddAzureTableStorage(tableConfiguration);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			var payloadWriteRepositoryImpl = serviceProvider.GetRequiredService<ILogReadRepository>();

			payloadWriteRepositoryImpl.Should()
				.NotBeNull()
				.And.BeOfType<LogRepository>();
		}

		[Fact]
		public void AddAzureTableStorage_WhenResolvingILogWriteRepository_ShouldBeResolved()
		{
			var serviceCollection = new ServiceCollection();
			var tableConfiguration = new AzureTableConfiguration("UseDevelopmentStorage=true", "test");

			serviceCollection.AddAzureTableStorage(tableConfiguration);

			var serviceProvider = serviceCollection.BuildServiceProvider();

			var payloadWriteRepositoryImpl = serviceProvider.GetRequiredService<ILogWriteRepository>();

			payloadWriteRepositoryImpl.Should()
				.NotBeNull()
				.And.BeOfType<LogRepository>();
		}
	}
}
