using Consid.Domain.Services.Abstractions;
using Consid.Infrastucture.AzureTableStorage.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Consid.Infrastucture.AzureTableStorage.IoC
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddAzureTableStorage(this IServiceCollection serviceCollection, AzureTableConfiguration configuration)
		{
			var repository = new LogRepository(new Azure.Data.Tables.TableClient(configuration.Connection, configuration.TableName));

			return serviceCollection
				.AddSingleton(repository)
				.AddSingleton<ILogReadRepository>(provider => provider.GetRequiredService<LogRepository>())
				.AddSingleton<ILogWriteRepository>(provider => provider.GetRequiredService<LogRepository>());
		}
	}
}
