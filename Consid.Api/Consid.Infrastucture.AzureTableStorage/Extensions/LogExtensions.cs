using Consid.Domain.Models;
using Consid.Infrastucture.AzureTableStorage.Entities;

namespace Consid.Infrastucture.AzureTableStorage.Extensions
{
	internal static class LogExtensions
	{
		public static LogTableEntity MapToEntity(this Log model) => new(model.LogDateTime, model.HttpStatusCode, model.PayloadAddress);
	}
}
