using Route256.Week1.Homework.PriceCalculator.Api.Bll.Models.PriceCalculator;

namespace Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
    /// <summary>
    /// Вычисляет стоимость доставки основываясь на:
    /// весе, объеме товаров, а также на расстоянии доставки.
    /// </summary>
    /// <param name="goods">Список доставляемых товаров</param>
    /// <param name="distance">Расстояние, на которое доставляется товар</param>
    decimal CalculatePrice(IReadOnlyList<GoodModel> goods, int distance);

    /// <summary>
    /// Делает запрос к хранилищу с логами расчетов стоимостей доставок.
    /// </summary>
    /// <param name="take">Количество возвращаемых записей из логов</param>
    CalculationLogModel[] QueryLog(int take);
}