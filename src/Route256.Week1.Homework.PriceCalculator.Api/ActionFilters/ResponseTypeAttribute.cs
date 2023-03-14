using Microsoft.AspNetCore.Mvc;

namespace Route256.Week1.Homework.PriceCalculator.Api.ActionFilters;

internal sealed class ResponseTypeAttribute : ProducesResponseTypeAttribute
{
	public ResponseTypeAttribute(int statusCode)
		: base(typeof(ErrorResponse), statusCode)
	{
	}
}