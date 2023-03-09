using Microsoft.AspNetCore.Mvc;
using Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;
using Route256.Week1.Homework.PriceCalculator.Api.Requests.V1;
using Route256.Week1.Homework.PriceCalculator.Api.Responses.V1;

namespace Route256.Week1.Homework.PriceCalculator.Api.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class V1OrderPriceController : ControllerBase
{
	private readonly ITotalPriceCalculatorService _totalPriceCalculatorService;

	public V1OrderPriceController(
		ITotalPriceCalculatorService totalPriceCalculatorService)
	{
		_totalPriceCalculatorService = totalPriceCalculatorService;
	}

	[HttpPost("calculate-total")]
	public CalculateResponse CalculateTotal(CalculateTotalRequest request)
	{
		return new(
			Price: _totalPriceCalculatorService.CalculateTotalPrice(
				request.goodId, request.distance));
	}
}
