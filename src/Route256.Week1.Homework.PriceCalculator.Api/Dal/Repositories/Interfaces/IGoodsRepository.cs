using Route256.Week1.Homework.PriceCalculator.Api.Dal.Entities;

namespace Route256.Week1.Homework.PriceCalculator.Api.Dal.Repositories.Interfaces;

public interface IGoodsRepository
{
	/// <summary>
	/// ��������� ��� �������� ����� � ���������.
	/// </summary>
	/// <param name="entity">�����</param>
	void AddOrUpdate(GoodEntity entity);

	/// <summary>
	/// �������� ��� ������������ ������� �� ���������.
	/// </summary>
	ICollection<GoodEntity> GetAll();

	/// <summary>
	/// �������� ����� � ������������ ��������������� �� ���������.
	/// </summary>
	/// <param name="id">������������� ������ � ���������</param>
	GoodEntity Get(int id);

	/// <summary>
	/// ���������� ������������� ������������ ������ � ��������� �� id.
	/// </summary>
	/// <param name="id">������������� ������ � ���������</param>
	bool Contains(int id);
}