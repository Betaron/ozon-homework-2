using Route256.Week1.Homework.PriceCalculator.Api.Bll.Models.PriceCalculator;

namespace Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;

public interface IPriceCalculatorService
{
	/// <summary>
	/// ��������� ��������� �������� ����������� ��:
	/// ����, ������ �������, � ����� �� ���������� ��������.
	/// </summary>
	/// <param name="goods">������ ������������ �������</param>
	/// <param name="distance">����������, �� ������� ������������ �����</param>
	decimal CalculatePrice(IReadOnlyList<GoodModel> goods, int distance);

	/// <summary>
	/// ������ ������ � ��������� � ������ �������� ���������� ��������.
	/// </summary>
	/// <param name="take">���������� ������������ ������� �� �����</param>
	CalculationLogModel[] QueryLog(int take);
}