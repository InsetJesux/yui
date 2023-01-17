using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Yui
{
    public class Yui
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _services;
        private readonly DiscordSocketConfig _socketConfig;
<<<<<<< HEAD
        private readonly DiscordSocketClient _client;
=======

        /// <summary>
        /// Crea una nueva instancia del bot
        /// </summary>
        /// <param name="configuration">Si es <see langword="null"/> se creará un <see cref="IConfiguration"/> por defecto</param>
        /// <param name="services">Si es <see langword="null"/> se creará un <see cref="IServiceProvider"/> por defecto</param>
        /// <param name="socketConfig">Si es <see langword="null"/> se creará un <see cref="DiscordSocketConfig"/> por defecto</param>
>>>>>>> 9a297df7f8a278d485150ea63cba53f9cb819288
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
<<<<<<< HEAD
        private DiscordSocketClient BuildClient()
        {
            return new DiscordSocketClient(_socketConfig);
        }
=======

        /// <summary>
        /// Crea un <see cref="IServiceProvider"/> por defecto
        /// </summary>
        /// <returns><see cref="IServiceProvider"/> por defecto</returns>
>>>>>>> 9a297df7f8a278d485150ea63cba53f9cb819288
        private IServiceProvider BuildServices()
        {
            return new ServiceCollection()
                .AddSingleton(_configuration)
                .AddSingleton(_socketConfig)
                .AddSingleton(_client)
                //.AddSingleton<DiscordSocketClient>()
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionHandler>()
                .BuildServiceProvider();
        }
<<<<<<< HEAD
        
=======

        /// <summary>
        /// Crea un <see cref="DiscordSocketConfig"/> por defecto
        /// </summary>
        /// <returns><see cref="DiscordSocketConfig"/> por defecto</returns>
>>>>>>> 9a297df7f8a278d485150ea63cba53f9cb819288
        private DiscordSocketConfig BuildSocketConfig()
        {
            return new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
                AlwaysDownloadUsers = true,
            };
        }

        private async Task LogAsync(LogMessage message)
            => Console.WriteLine(message.ToString());
    }
}