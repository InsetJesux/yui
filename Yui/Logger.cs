using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SerilogLogger = Serilog.Core.Logger;

namespace Yui
{
    public class Logger
    {
        public Logger()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Version version = assembly.GetName().Version;
            string projectName = assembly.GetName().Name;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Version", version)
                .Enrich.WithProperty("ProjectName", projectName)
                .WriteTo.Async(wt => wt.Logger(BuildConsoleLogger()))
                .WriteTo.Async(wt => wt.Logger(BuildFileLogger()))
                .WriteTo.Async(wt => wt.Logger(BuildExcepcionLogger()))
                .CreateLogger();
        }

        private SerilogLogger BuildConsoleLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss}] [{ProjectName} v{Version}] [{Level:u4}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }

        private SerilogLogger BuildFileLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.File(
                    path: @"logs\log-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{ProjectName} v{Version}] [{Level:u4}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }

        private SerilogLogger BuildExcepcionLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(Serilog.Events.LogEventLevel.Fatal)
                .Enrich.WithExceptionDetails()
                .WriteTo.File(
                    path: @"logs\exceptions\exception-.json",
                    formatter: new CompactJsonFormatter(),
                    rollingInterval: RollingInterval.Minute
                )
                .CreateLogger();
        }
    }
}
