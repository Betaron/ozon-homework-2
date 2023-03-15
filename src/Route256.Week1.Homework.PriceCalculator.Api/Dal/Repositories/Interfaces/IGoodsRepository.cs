using Route256.Week1.Homework.PriceCalculator.Api.Dal.Entities;

namespace Route256.Week1.Homework.PriceCalculator.Api.Dal.Repositories.Interfaces;

public interface IGoodsRepository
{
    /// <summary>
    /// Добавляет или изменяет товар в хранилище.
    /// </summary>
    /// <param name="entity">Товар</param>
    void AddOrUpdate(GoodEntity entity);

    /// <summary>
    /// Получает все наименования товаров из хранилища.
    /// </summary>
    ICollection<GoodEntity> GetAll();

    /// <summary>
    /// Получает товар с определенным идентификатором из хранилища.
    /// </summary>
    /// <param name="id">Идентификатор товара в хранилище</param>
    GoodEntity Get(int id);

    /// <summary>
    /// Определяет существование наименование товара в хранилище по id.
    /// </summary>
    /// <param name="id">Идентификатор товара в хранилище</param>
    bool Contains(int id);
}