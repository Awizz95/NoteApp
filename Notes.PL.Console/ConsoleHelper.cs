using Serilog;

namespace Epam.Notes.PL.Console
{
    public static class SerilogHelper
    {
        public static void InitializeLogger(bool fileLogs = false, string fileName = "logs/myapp.txt")
        {
            if (fileLogs)
                Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.Console()
                            .WriteTo.File(fileName)
                            .CreateLogger();
            else
                Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.Console()
                            .CreateLogger();
        }
    }
}
