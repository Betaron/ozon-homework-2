using Route256.Week1.Homework.PriceCalculator.Api.Dal.Entities;

namespace Route256.Week1.Homework.PriceCalculator.Api.Dal.Repositories.Interfaces;

public interface IStorageRepository
{
    /// <summary>
    /// Добавляет информацию о расчете стоимости доставки товара.
    /// </summary>
    /// <param name="entity">Товар</param>
    void Save(StorageEntity entity);

    /// <summary>
    /// Запрашивает список сохраненных записей 
    /// о расчетах стоимости доставки.
    /// </summary>
    IReadOnlyList<StorageEntity> Query();
}