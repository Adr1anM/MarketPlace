using MarketPlace.Application;
using MarketPlace.Application.FileServices;
using MarketPlace.Infrastructure.FileSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class FileLoggingBackgroundService : BackgroundService
{
    private readonly ILogger<FileLoggingBackgroundService> _logger;
    private readonly IFileService _fileHandlerService;
    private readonly IFileLogger _fileLogger;
    private readonly IServiceScopeFactory _scopeFactoryService;
    private readonly IConfiguration _configuration;

    public FileLoggingBackgroundService(ILogger<FileLoggingBackgroundService> logger, IFileService fileHandlerService, IFileLogger fileLogger, IServiceScopeFactory scopedfactory, IConfiguration configuration)
    {
        _logger = logger;
        _fileHandlerService = fileHandlerService;
        _fileLogger = fileLogger;
        _scopeFactoryService = scopedfactory;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactoryService.CreateScope())
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();
                    var fileLogger = scope.ServiceProvider.GetRequiredService<IFileLogger>();

                    bool result = await CompareTofileData("sadasda");
                   
                    if (result)
                    {
                        await fileLogger.LogSuccess("Method execution succeeded.");
                    }
                    else
                    {
                        await fileLogger.LogFailure("Method execution failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during method execution.");
            }

          
            await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
        }
    }

    public async Task<bool> CompareTofileData(string template)
    {
        using (var scope = _scopeFactoryService.CreateScope())
        {
            var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

            string path = _configuration.GetValue<string>("FileDirectories:TextFilePath")!;
            string data = await fileService.GetFile(path);

            if (data.Contains(template))
            {
                return true;
            }
        }

        return false;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("FileLoggingBackgroundService is stopping.");
        await base.StopAsync(cancellationToken);
    }
}
