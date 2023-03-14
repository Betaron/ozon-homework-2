using System.Text;
using Microsoft.AspNetCore.Http.Extensions;

namespace Route256.Week1.Homework.PriceCalculator.Api.Middlewaries;

internal sealed class TotalPriceLogMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<TotalPriceLogMiddleware> _logger;

	public TotalPriceLogMiddleware(
		RequestDelegate next,
		ILogger<TotalPriceLogMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		if (context.Request.Path.StartsWithSegments("/v1/V1OrderPrice/calculate-total")
			&& context.Request.Method == "POST")
		{
			var requestBodyContent = await ReadStreamAsync(context.Request.Body);

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

			var responceBodyContent = await ReadStreamAsync(responseStream);
			context.Response.Body = originalResponseBodyStream;
			await responseStream.CopyToAsync(context.Response.Body);

			var logMessage = new StringBuilder();

			logMessage.AppendLine($"{Environment.NewLine}Request: ");
			logMessage.AppendLine($"\tTimestamp: {requestInfo.Timestamp}");
			logMessage.AppendLine($"\tUrl: {requestInfo.Url}");
			logMessage.AppendLine($"\tHeaders:");
			foreach (var header in requestInfo.Headers)
			{
				logMessage.AppendLine($"\t\t{header.Key}: {header.Value}");
			}
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

	private async Task<string> ReadStreamAsync(Stream targetStream)
	{
		var streamContent = string.Empty;
		using (var sr = new StreamReader(
			targetStream, Encoding.UTF8, leaveOpen: true))
		{
			streamContent = await sr.ReadToEndAsync();
		}

		return streamContent;
	}
}
