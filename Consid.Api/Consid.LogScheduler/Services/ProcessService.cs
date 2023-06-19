using Consid.Domain.Models;
using Consid.Domain.Services.Abstractions;
using Consid.LogScheduler.Dtos;
using Consid.LogScheduler.Extensions;
using System;
using System.Threading.Tasks;

namespace Consid.LogScheduler.Services
{
	public class ProcessService : IProcessService
	{
		protected readonly ILogWriteRepository _logRepository;
		protected readonly IPayloadWriteRepository _payloadRepository;

		public ProcessService(ILogWriteRepository logRepository, IPayloadWriteRepository payloadRepository)
		{
			_logRepository = logRepository;
			_payloadRepository = payloadRepository;
		}

		public async Task StartProcessAsync(ApiResponseDto data)
		{
			var processDateTime = GetProcessDate();

			var log = CreateLogModel(processDateTime, data.HttpStatus);

			var payload = CreatePayloadModel(log.PayloadAddress, data.Content);

			var payloadSaveTask = _payloadRepository.SaveAsync(payload);

			var logSaveTask = _logRepository.SaveLogAsync(log);

			await Task.WhenAll(payloadSaveTask, logSaveTask);
		}

		private static Payload CreatePayloadModel(string payloadAddress, string content)
		{
			return new Payload(payloadAddress, content);
		}

		private static DateTimeOffset GetProcessDate() => DateTimeOffset.UtcNow;

		private static Log CreateLogModel(DateTimeOffset processDateTime, int responseStatus) {
			var logId = processDateTime.ToLogId();
			return new Log(logId, processDateTime, responseStatus, logId);
		}
	}
}
