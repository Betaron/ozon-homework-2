using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
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
            await using var responceStream = new MemoryStream();

            var originalResponseBodyStream = context.Response.Body;
            context.Response.Body = responceStream;

            await _next.Invoke(context);

            await Log(context.Request, context.Response);

            responceStream.Position = 0;

            await responceStream.CopyToAsync(originalResponseBodyStream);
            context.Response.Body = originalResponseBodyStream;
        }
        else
        {
            await _next.Invoke(context);
        }
    }

    private static async Task<string> ReadStreamAsync(Stream stream)
    {
        var initialPosition = stream.Position;

        stream.Position = 0;
        using var sr = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        var content = await sr.ReadToEndAsync();

        stream.Position = initialPosition;

        return content;
    }

    private async Task Log(HttpRequest request, HttpResponse response)
    {
        var requestBodyContent = await ReadStreamAsync(request.Body);
        var responseBodyContent = await ReadStreamAsync(response.Body);

        var logModel = new LogModel(
            DateTime.UtcNow,
            request.GetDisplayUrl(),
            request.Headers,
            requestBodyContent,
            responseBodyContent);

        Log(logModel);
    }

    private void Log(LogModel model)
    {
        var logMessage = new StringBuilder();

        logMessage.AppendLine($"{Environment.NewLine}Request: ");
        logMessage.AppendLine($"\tTimestamp: {model.Timestamp}");
        logMessage.AppendLine($"\tUrl: {model.Url}");
        logMessage.AppendLine($"\tHeaders:");
        foreach (var header in model.Headers)
        {
            logMessage.AppendLine($"\t\t{header.Key}: {header.Value}");
        }
        logMessage.AppendLine($"\tBody: {model.RequestBody}");
        logMessage.AppendLine($"Response:");
        logMessage.AppendLine($"\tBody: {model.ResponseBody}");

        _logger.LogInformation(logMessage.ToString());
    }

    private sealed record LogModel(
        DateTime Timestamp,
        string Url,
        IDictionary<string, StringValues> Headers,
        string RequestBody,
        string ResponseBody
    );
}
