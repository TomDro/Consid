using Consid.WebApi.Dtos;
using Consid.WebApi.Services.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Consid.WebApi.Tests.Services.Validators
{
	public class LogQueryParametersValidatorTests
	{
		private readonly LogQueryParametersValidator _validator;

		public LogQueryParametersValidatorTests()
		{
			_validator = new LogQueryParametersValidator();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Validate_WhenDateFromIsNullOrEmpty_MustFailWithNotEmptyValidatorRule(string from)
		{
			var dto = new LogQueryParameters(from, "2023-06-01");

			_validator.TestValidate(dto)
				.ShouldHaveValidationErrorFor(x => x.From)
				.WithErrorMessage("'From' is required");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Validate_WhenDateToIsNullOrEmpty_MustFailWithNotEmptyValidatorRule(string to)
		{
			var dto = new LogQueryParameters("2023-06-01", to);

			_validator.TestValidate(dto)
				.ShouldHaveValidationErrorFor(x => x.To)
				.WithErrorMessage("'To' is required");
		}

		[Theory]
		[InlineData("20221-01-01")]
		[InlineData("text")]
		public void Validate_WhenDateFromIsInvalid_MustFailWithNotEmptyValidatorRule(string from)
		{
			var dto = new LogQueryParameters(from, "2023-06-01");

			_validator.TestValidate(dto)
				.ShouldHaveValidationErrorFor(x => x.From)
				.WithErrorMessage("'From' has invalid format");
		}

		[Theory]
		[InlineData("20221-01-01")]
		[InlineData("text")]
		public void Validate_WhenDateToIsInvalid_MustFailWithNotEmptyValidatorRule(string to)
		{
			var dto = new LogQueryParameters("2023-06-01", to);

			_validator.TestValidate(dto)
				.ShouldHaveValidationErrorFor(x => x.To)
				.WithErrorMessage("'To' has invalid format");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Validate_WhenAllDatesAreNull_MustFailWithNotEmptyValidatorRule(string date)
		{
			var dto = new LogQueryParameters(date, date);

			var testResult = _validator.TestValidate(dto);

			testResult.ShouldHaveValidationErrorFor(x => x.From)
				.WithErrorCode("NotEmptyValidator");

			testResult.ShouldHaveValidationErrorFor(x => x.To)
				.WithErrorCode("NotEmptyValidator");
		}

		[Theory]
		[InlineData("2023-06-02", "2023-06-01")]
		[InlineData("2023-06-02 12:14:54", "2023-06-02 12:12:54")]
		public void Validate_WhenDateToIsNotGreaterThatDateFrom_MustFailWithNotEmptyValidatorRule(string dateFrom, string dateTo)
		{
			var dto = new LogQueryParameters(dateFrom, dateTo);

			var testResult = _validator.TestValidate(dto);

			testResult.ShouldHaveValidationErrorFor(x => x)
				.WithErrorMessage("'To' must be greater than or equal to 'From'");
		}

		[Theory]
		[InlineData("2023-06-02", "2023-06-02")]
		[InlineData("2023-06-02", "2023-06-03")]
		[InlineData("2023-06-02 12:14:54", "2023-06-02 12:15:54")]
		public void Validate_WhenDatesAreValid_MustNotFail(string dateFrom, string dateTo)
		{
			var dto = new LogQueryParameters(dateFrom, dateTo);

			var testResult = _validator.TestValidate(dto);

			testResult.ShouldNotHaveAnyValidationErrors();
		}
	}
}
