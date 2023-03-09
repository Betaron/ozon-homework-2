using Route256.Week1.Homework.PriceCalculator.Api.Bll.Models.PriceCalculator;
using Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;
using Route256.Week1.Homework.PriceCalculator.Api.Dal.Repositories.Interfaces;

namespace Route256.Week1.Homework.PriceCalculator.Api.Bll.Services;

public class TotalPriceCalculatorService : ITotalPriceCalculatorService
{
	private readonly IGoodsRepository _goodsRepository;
	private readonly IPriceCalculatorService _priceCalculatorService;

	public TotalPriceCalculatorService(
		IGoodsRepository goodsRepository,
		IPriceCalculatorService priceCalculatorService)
	{
		_goodsRepository = goodsRepository;
		_priceCalculatorService = priceCalculatorService;
	}

	public decimal CalculateTotalPrice(int goodId, int distance)
	{
		var good = _goodsRepository.Get(goodId);
		var model = new GoodModel(
			good.Height,
			good.Length,
			good.Width,
			good.Weight);

		var deliveryPrice =
			_priceCalculatorService.CalculatePrice(new[] { model }, distance);

		return deliveryPrice + good.Price;
	}
}
