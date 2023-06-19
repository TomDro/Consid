using System;

namespace Consid.Domain.Exceptions
{
	public class ResourceNotFoundException : Exception
	{
		private static readonly string _messageTemplate = "Resource {0} not found";
		public ResourceNotFoundException(string? resourceId) : this(resourceId, null)
		{
		}

		public ResourceNotFoundException(string? resourceId, Exception? innerException) : base(GetMessage(resourceId), innerException)
		{
		}

		private static string GetMessage(string? resourceId)
		{
			return string.Format(_messageTemplate, resourceId ?? string.Empty);
		}
	}
}
