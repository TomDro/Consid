using Consid.WebApi.Dtos;
using FluentValidation;
using System;

namespace Consid.WebApi.Services.Validators
{
	internal class LogQueryParametersValidator : AbstractValidator<LogQueryParameters>
	{
		private static readonly string _requiredMsgTemplate = "'{0}' is required";
		private static readonly string _invalidFormatMsgTemplate = "'{0}' has invalid format";
		private static readonly string _invalidRangeMsg = "'To' must be greater than or equal to 'From'";
		public LogQueryParametersValidator()
		{
			var dateFromIsValid = false;
			var dateToIsValid = false;

			RuleLevelCascadeMode = CascadeMode.Stop;

			RuleFor(x => x.From)
				.NotEmpty()
				.WithMessage(GetRequiredMsg(nameof(LogQueryParameters.From)))
				.Must((value) =>
				{
					dateFromIsValid = DateTimeOffset.TryParse(value, out var _);
					return dateFromIsValid;
				})
				.WithMessage(GetInvalidFormatMsg(nameof(LogQueryParameters.From)));

			RuleFor(x => x.To)
				.NotEmpty()
				.WithMessage(GetRequiredMsg(nameof(LogQueryParameters.To)))
				.Must((value) =>
				{
					dateToIsValid = DateTimeOffset.TryParse(value, out var _);
					return dateToIsValid;
				})
				.WithMessage(GetInvalidFormatMsg(nameof(LogQueryParameters.To)));

			When((_) => dateFromIsValid && dateToIsValid, () =>
			{
				RuleFor(x => x).Must(x =>
				{
					var from = DateTimeOffset.Parse(x.From);
					var to = DateTimeOffset.Parse(x.To);

					return from <= to;
				})
				.WithMessage(_invalidRangeMsg);
			});
		}

		private static string GetInvalidFormatMsg(string propName) => string.Format(_invalidFormatMsgTemplate, propName);
		private static string GetRequiredMsg(string propName) => string.Format(_requiredMsgTemplate, propName);
	}
}
