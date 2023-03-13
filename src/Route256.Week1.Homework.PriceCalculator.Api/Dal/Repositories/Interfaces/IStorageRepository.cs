using Route256.Week1.Homework.PriceCalculator.Api.Dal.Entities;

namespace Route256.Week1.Homework.PriceCalculator.Api.Dal.Repositories.Interfaces;

public interface IStorageRepository
{
    /// <summary>
    /// ��������� ���������� � ������� ��������� �������� ������.
    /// </summary>
    /// <param name="entity">�����</param>
    void Save(StorageEntity entity);

    /// <summary>
    /// ����������� ������ ����������� ������� 
    /// � �������� ��������� ��������.
    /// </summary>
    IReadOnlyList<StorageEntity> Query();
}