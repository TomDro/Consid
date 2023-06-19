using Consid.Infrastucture.AzureBlobStorage.IoC;
using Consid.Infrastucture.AzureTableStorage.IoC;
using Consid.WebApi.Dtos;
using Consid.WebApi.Middlewares;
using Consid.WebApi.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
	.ConfigureFunctionsWorkerDefaults(workerApplication =>
	{
		workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
		workerApplication.UseMiddleware<ResponseHeadersMiddleware>();
	})
	.ConfigureServices((context, services) =>
	{
		var configuration = context.Configuration;

		services
			.AddAzureTableStorage(new AzureTableConfiguration(configuration["AzureTableLogStorage"], "logs"))
			.AddBlobStorage(new AzureBlobConfiguration(configuration["AzureBlobStorage"], "logs"))
			.AddScoped<IValidator<LogQueryParameters>, LogQueryParametersValidator>();
	})
	.Build();

host.Run();
