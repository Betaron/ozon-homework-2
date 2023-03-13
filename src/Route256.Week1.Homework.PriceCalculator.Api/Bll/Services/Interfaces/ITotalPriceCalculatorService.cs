namespace Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;

public interface ITotalPriceCalculatorService
{
	/// <summary>
	/// Вычисляет полную стоимость за заказ.
	/// Складывает стоимоть товара и сумму доставки.
	/// </summary>
	/// <param name="goodId">Идентификатор товара в репозитории товаров</param>
	/// <param name="distance">Расстояние, на которое доставляется товар</param>
	decimal CalculateTotalPrice(int goodId, int distance);
}
