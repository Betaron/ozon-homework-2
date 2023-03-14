using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Route256.Week1.Homework.PriceCalculator.Api.Attributes;

namespace Route256.Week1.Homework.PriceCalculator.Api.Middlewaries;

internal sealed class ApiMethodLogMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<TotalPriceLogMiddleware> _logger;

	public ApiMethodLogMiddleware(
		RequestDelegate next,
		ILogger<TotalPriceLogMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
		var attribute = endpoint?.Metadata.GetMetadata<LogApiMethodInfoAttribute>();

		if (attribute != null)
		{
			context.Request.Body.Position = 0;
			var requestReader = new StreamReader(context.Request.Body);
			var requestBodyContent = await requestReader.ReadToEndAsync();
			context.Request.Body.Position = 0;

			var requestInfo = new
			{
				Timestamp = DateTime.UtcNow.ToString(),
				Url = context.Request.GetDisplayUrl(),
				Headers = context.Request.Headers,
				Body = requestBodyContent
			};

			var originalResponseBodyStream = context.Response.Body;
			await using var responseStream = new MemoryStream();
			context.Response.Body = responseStream;

			await _next.Invoke(context);

			requestReader.Close();

			responseStream.Position = 0;
			var responseReader = new StreamReader(responseStream);
			var responceBodyContent = await responseReader.ReadToEndAsync();

			responseStream.Position = 0;
			context.Response.Body = originalResponseBodyStream;
			await responseStream.CopyToAsync(context.Response.Body);

			responseReader.Close();

			var logMessage = new StringBuilder();

			logMessage.AppendLine($"{Environment.NewLine}Request: ");
			logMessage.AppendLine($"\tTimestamp: {requestInfo.Timestamp}");
			logMessage.AppendLine($"\tUrl: {requestInfo.Url}");
			logMessage.AppendLine($"\tHeaders:");
			foreach (var header in requestInfo.Headers)
				logMessage.AppendLine($"\t\t{header.Key}: {header.Value}");
			logMessage.AppendLine($"\tBody: {requestInfo.Body}");
			logMessage.AppendLine($"Response:");
			logMessage.AppendLine($"\tBody: {responceBodyContent}");

			_logger.LogInformation(logMessage.ToString());
		}
		else
		{
			await _next.Invoke(context);
		}
	}
}
