using Consid.Domain.Models;
using Consid.Infrastucture.AzureTableStorage.Entities;

namespace Consid.Infrastucture.AzureTableStorage.Extensions
{
	internal static class LogTableEntityExtensions
	{
		public static Log MapToModel(this LogTableEntity entity) => new(entity.RowKey, entity.LogDateTime, entity.HttpStatus, entity.PayloadAddress);
	}
}
