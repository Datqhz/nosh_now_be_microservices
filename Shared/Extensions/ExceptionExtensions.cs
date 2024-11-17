using Microsoft.Extensions.Logging;

namespace Shared.Extensions;

public static class ExceptionExtensions
{
    public static void LogError<T>(this Exception exception, string functionName, ILogger<T> logger)
    {
        logger.LogError(exception, $"{functionName} Has error: Message {exception.Message}");
    }
}