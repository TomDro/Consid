using Consid.LogScheduler.Dtos;
using Consid.LogScheduler.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Consid.Service
{
	public class LogDownloader
    {
        private readonly ILogger<LogDownloader> _logger;
		private readonly IDataApiGateway _dataApiGateway;
        private readonly IProcessService _processService;
		public LogDownloader(ILogger<LogDownloader> logger,
            IDataApiGateway dataApiGateway,
            IProcessService processService)
        {
            _logger = logger;
            _dataApiGateway = dataApiGateway;
            _processService = processService;
        }

        [Function("LogDownloader")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTimeOffset.UtcNow}");

            try
            {
                var response = await _dataApiGateway.GetDataAsync();

                await _processService.StartProcessAsync(response);

                _logger.LogInformation("Log saved");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }
}
