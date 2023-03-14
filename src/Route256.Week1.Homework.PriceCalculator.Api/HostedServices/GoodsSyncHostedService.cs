using Microsoft.Extensions.Options;
using Route256.Week1.Homework.PriceCalculator.Api.Bll.Options;
using Route256.Week1.Homework.PriceCalculator.Api.Bll.Services.Interfaces;
using Route256.Week1.Homework.PriceCalculator.Api.Dal.Repositories.Interfaces;

namespace Route256.Week1.Homework.PriceCalculator.Api.HostedServices;

public sealed class GoodsSyncHostedService : BackgroundService, IDisposable
{
	private readonly IGoodsRepository _repository;
	private readonly IServiceProvider _serviceProvider;
	private readonly IDisposable? _optionsChangeListner;

	private double _stockSyncPeriod;

	public GoodsSyncHostedService(
		IGoodsRepository repository,
		IServiceProvider serviceProvider,
		IOptionsMonitor<GoodsSyncOptions> options)
	{
		_repository = repository;
		_serviceProvider = serviceProvider;

		_optionsChangeListner = options.OnChange(config =>
		{
			_stockSyncPeriod = config.StockSyncPeriod;
		});
		_stockSyncPeriod = options.CurrentValue.StockSyncPeriod;
	}

	public override void Dispose()
	{
		_optionsChangeListner?.Dispose();
		base.Dispose();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var goodsService = scope.ServiceProvider.GetRequiredService<IGoodsService>();
				var goods = goodsService.GetGoods().ToList();
				foreach (var good in goods)
					_repository.AddOrUpdate(good);
			}

			await Task.Delay(TimeSpan.FromSeconds(_stockSyncPeriod), stoppingToken);
		}
	}
}