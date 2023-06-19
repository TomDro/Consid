using Azure.Data.Tables;
using Consid.Domain.Models;
using Consid.Domain.Services.Abstractions;
using Consid.Infrastucture.AzureTableStorage.Entities;
using Consid.Infrastucture.AzureTableStorage.Extensions;

namespace Consid.Infrastucture.AzureTableStorage.Repositories
{
	public class LogRepository : ILogReadRepository, ILogWriteRepository
	{
		private readonly TableClient _tableClient;

		public LogRepository(TableClient tableClient)
		{
			_tableClient = tableClient;
		}

		public async Task<Log[]> GetLogsAsync(DateTimeOffset from, DateTimeOffset to)
		{
			var models = new List<Log>();
			var entities = _tableClient.QueryAsync<LogTableEntity>(l => l.LogDateTime >= from && l.LogDateTime <= to);

			await foreach (var entity in entities)
			{
				models.Add(entity.MapToModel());
			}

			return models.ToArray();
		}

		public async Task SaveLogAsync(Log log)
		{
			var entity = log.MapToEntity();
			await _tableClient.AddEntityAsync(entity);
		}
	}
}