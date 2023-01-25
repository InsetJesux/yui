using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Core;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Reflection;
using SerilogLogger = Serilog.Core.Logger;

namespace Yui
{
    /// <summary>
    /// Clase que controla los registros de la aplicación
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Controla el nivel al que se registran los eventos
        /// </summary>
        public static LoggingLevelSwitch Lever { get; private set; } = new LoggingLevelSwitch();

        /// <summary>
        /// Crea el <see cref="SerilogLogger"/> principal del programa
        /// </summary>
        /// <returns><see cref="SerilogLogger"/> principal del programa</returns>
        public static SerilogLogger BuildLogger()
        {
            // Obtener propiedades del proyecto
            Assembly assembly = Assembly.GetEntryAssembly();
            Version version = assembly.GetName().Version;
            string projectName = assembly.GetName().Name;

            // Cambiar el lever a verbose si estamos en Debug
            Lever.MinimumLevel = Program.IsDebug() ? LogEventLevel.Verbose : LogEventLevel.Information;

            // Crear logger con escritura asincrona, controlando el nivel de log mediante Lever y pasando las propiedades del proyecto
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Version", version)
                .Enrich.WithProperty("ProjectName", projectName)
                .WriteTo.Async(wt => wt.Logger(BuildConsoleLogger()))
                .WriteTo.Async(wt => wt.Logger(BuildFileLogger()))
                .WriteTo.Async(wt => wt.Logger(BuildExceptionLogger()))
                .CreateLogger();
        }

        /// <summary>
        /// Crea el sublogger encargado de registrar en la consola
        /// </summary>
        /// <returns><see cref="SerilogLogger"/> sublogger que registra en la consola</returns>
        private static SerilogLogger BuildConsoleLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.ControlledBy(Lever)
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss}] [{ProjectName} v{Version}] [{Level:u4}] {Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Sixteen
                )
                .CreateLogger();
        }

        /// <summary>
        /// Crea el sublogger encargado de registrar en archivos
        /// </summary>
        /// <returns><see cref="SerilogLogger"/> sublogger que registra en archivos</returns>
        private static SerilogLogger BuildFileLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.ControlledBy(Lever)
                .WriteTo.File(
                    path: @"logs\log-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{ProjectName} v{Version}] [{Level:u4}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }


        /// <summary>
        /// Crea el sublogger encargado de registrar excepciones en archivos
        /// </summary>
        /// <returns><see cref="SerilogLogger"/> sublogger que registra excepciones en archivos</returns>
        private static SerilogLogger BuildExceptionLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Fatal)
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
