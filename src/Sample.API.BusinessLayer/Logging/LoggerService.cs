using Microsoft.Extensions.Logging;

namespace Sample.API.BusinessLayer.Logging;

public static class LoggerService
{
    private static ILoggerFactory loggerFactory;

    public static void Init(ILoggerFactory factory) => loggerFactory = factory;

    public static ILogger GetLogger<T>() => loggerFactory.CreateLogger<T>();
}