using Consid.LogScheduler.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Consid.LogScheduler.Tests.Extensions
{
	public class DateTimeOffsetExtensionsTests
	{
		[Theory]
		[MemberData(nameof(GetData))]
		public void ToLogId_MustBeValid(DateTimeOffset sourceDate, string expectedValue)
		{
			var result = sourceDate.ToLogId();

			result.Should()
				.Be(expectedValue);
		}

		public static IEnumerable<object[]> GetData =>
			new List<object[]>
			{
				new object[]{ new DateTimeOffset(2023, 01, 15, 15, 44, 07, TimeSpan.Zero) , "20230115154407"},
				new object[]{ new DateTimeOffset(2023, 01, 21, 00, 00, 00, TimeSpan.Zero) , "20230121000000"},
				new object[]{ new DateTimeOffset(2023, 01, 21, 00, 00, 00, TimeSpan.FromHours(1)), "20230121000000"},
			};
	}
}
