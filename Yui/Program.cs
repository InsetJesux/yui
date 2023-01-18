using Serilog;
using Serilog.Core;
using System.Reflection;
using System;
using System.Runtime.CompilerServices;
///Este archivo representa la desesperacion sufrida por Velka el dia 16-01-2023 en una terrible y sangrienta mañana. El lado positivo es que Pyrus aprobo el bot
namespace Yui
{
    public class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();

            try
            {
                new Yui().RunAsync()
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Fatal exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }
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
