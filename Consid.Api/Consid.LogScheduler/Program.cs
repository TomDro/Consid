using Consid.Infrastucture.AzureBlobStorage.IoC;
using Consid.Infrastucture.AzureTableStorage.IoC;
using Consid.LogScheduler.Configuration;
using Consid.LogScheduler.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices((context, services) =>
	{
		var configuration = context.Configuration;

		services
			.AddOptions<DataSourceOptions>()
				.Configure(dso => dso.DataSourceUrl = configuration.GetSection("DataSourceUrl").Value);

		services
			.AddAzureTableStorage(new AzureTableConfiguration(configuration["AzureTableLogStorage"], "logs"))
			.AddBlobStorage(new AzureBlobConfiguration(configuration["AzureBlobStorage"], "logs"))
			.AddHttpClient()
			.AddScoped<IDataApiGateway, DataApiGateway>()
			.AddScoped<IProcessService, ProcessService>();

		services.BuildServiceProvider();
	})
	.Build();

host.Run();
