namespace Consid.LogScheduler.Extensions
{
	internal static class DateTimeOffsetExtensions
	{
		public static string ToLogId(this DateTimeOffset dateTimeOffset) => dateTimeOffset.ToString("yyyyMMddHHmmss");
	}
}
