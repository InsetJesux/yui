using Serilog;
using Serilog.Core;
///Este archivo representa la desesperacion sufrida por Velka el dia 16-01-2023 en una terrible y sangrienta mañana. El lado positivo es que Pyrus aprobo el bot
namespace Yui
{
    public class Program
    {
        static void Main(string[] args)
        {
            BuildLogger();

            try
            {
                new Yui().RunAsync()
                    .GetAwaiter()
                    .GetResult();
            }
            catch (System.Exception ex)
            {
                Log.Error("Excepción fatal: {ex}", ex);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void BuildLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Async(
                    l => l.File(
                        path: "logs/log.log", rollingInterval: RollingInterval.Minute)
                )
                .WriteTo.Async(
                    l => l.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                    )
                )
                .CreateLogger();
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}
