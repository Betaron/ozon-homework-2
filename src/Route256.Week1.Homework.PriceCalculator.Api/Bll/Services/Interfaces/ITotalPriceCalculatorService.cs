namespace Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;

public interface ITotalPriceCalculatorService
{
	decimal CalculateTotalPrice(int goodId, int distance);
}
