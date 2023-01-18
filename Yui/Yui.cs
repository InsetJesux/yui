using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Yui.Events.Listeners;

namespace Yui
{
    public class Yui
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;
        private readonly DiscordSocketConfig _socketConfig;
        private readonly DiscordSocketClient _client;
        private ChannelListener _channelListener;


        /// <summary>
        /// Crea una nueva instancia del bot
        /// </summary>
        /// <param name="configuration">Si es <see langword="null"/> se creará un <see cref="IConfiguration"/> por defecto</param>
        /// <param name="services">Si es <see langword="null"/> se creará un <see cref="IServiceProvider"/> por defecto</param>
        /// <param name="socketConfig">Si es <see langword="null"/> se creará un <see cref="DiscordSocketConfig"/> por defecto</param>
        public Yui(IConfiguration configuration = null, IServiceProvider services = null, DiscordSocketConfig socketConfig = null)
        {
            _configuration = configuration ?? BuildConfiguration();
            _socketConfig = socketConfig ?? BuildSocketConfig();
            _client = BuildClient();
            _services = services ?? BuildServices();
        }

        public async Task RunAsync()
        {
            var client = _services.GetRequiredService<DiscordSocketClient>();

            client.Log += LogAsync;

            // Inicializar servicio para registrar los comandos
            await _services.GetRequiredService<InteractionHandler>()
                .InitializeAsync();

            // Comprobar que el token esta configurado
            if (_configuration["token"] == null)
            {
                Log.Error("No se ha encontrado el token en la configuración");
                return;
            }

            // Iniciar la conexión con la gateway de Discord mediante el token obtenido de la configuración
            await client.LoginAsync(TokenType.Bot, _configuration["token"]);
            await client.StartAsync();

            // Mantener la ejecución
            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Crea un <see cref="IConfiguration"/> por defecto
        /// </summary>
        /// <returns><see cref="IConfiguration"/> por defecto</returns>
        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "DC_")
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }



        /// <summary>
        /// Crea una nueva instancia de <see cref="DiscordSocketClient"/> e implimentacion de los listeners necesarios de eventos en la misma
        /// </summary>
        /// <returns>Nueva instancia de <see cref="DiscordSocketClient"/></returns>

        private DiscordSocketClient BuildClient()
        {
            DiscordSocketClient client = new DiscordSocketClient(_socketConfig);
            _channelListener = new ChannelListener(client);
            client.UserVoiceStateUpdated += _channelListener.ClientUserVoiceStateUpdated;
            return client;
        }

        /// <summary>
        /// Crea un <see cref="IServiceProvider"/> por defecto
        /// </summary>
        /// <returns><see cref="IServiceProvider"/> por defecto</returns>
        private IServiceProvider BuildServices()
        {
            return new ServiceCollection()
                .AddSingleton(_configuration)
                .AddSingleton(_socketConfig)
                .AddSingleton(_client)
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionHandler>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Crea un <see cref="DiscordSocketConfig"/> por defecto
        /// </summary>
        /// <returns><see cref="DiscordSocketConfig"/> por defecto</returns>
        private DiscordSocketConfig BuildSocketConfig()
        {
            return new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
                AlwaysDownloadUsers = true,
            };
        }

        private async Task LogAsync(LogMessage message)
        {
            LogEventLevel severity = message.Severity switch
            {
                LogSeverity.Critical => LogEventLevel.Fatal,
                LogSeverity.Error => LogEventLevel.Error,
                LogSeverity.Warning => LogEventLevel.Warning,
                LogSeverity.Info => LogEventLevel.Information,
                LogSeverity.Verbose => LogEventLevel.Verbose,
                LogSeverity.Debug => LogEventLevel.Debug,
                _ => LogEventLevel.Information
            };
            Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);
            await Task.CompletedTask;
        }
    }
}