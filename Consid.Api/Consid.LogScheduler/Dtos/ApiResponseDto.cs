namespace Consid.LogScheduler.Dtos
{
	public record ApiResponseDto
	{
		public ApiResponseDto(int httpStatus, string content)
		{
			HttpStatus = httpStatus;
			Content = content;
		}

		public int HttpStatus { get; private set; }
		public string Content { get; private set; }
	}
}
