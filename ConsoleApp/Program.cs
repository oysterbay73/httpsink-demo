using Serilog;

Serilog.Debugging.SelfLog.Enable(Console.Error);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
        requestUri: "https://www.mylogs.com",        
        httpClient: null,
        bufferBaseFileName: "buffer",
        bufferFileShared: true)
    .CreateLogger();
var i = 1;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Sending logs");
do
{
    while (!Console.KeyAvailable)
    {
        Log.Information("Sending {index}", i);
        i++;
        Thread.Sleep(500);
    }
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);


Log.CloseAndFlush();


