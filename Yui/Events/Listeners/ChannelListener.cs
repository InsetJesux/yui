using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Yui.Events.Controllers;
using Yui.Models;

namespace Yui.Events.Listeners
{
    /// <summary>
    /// Clase encargada de controlar el evento del movimiento de usuarios en los canales de voz
    /// </summary>
    public sealed class ChannelListener
    {
        private readonly DiscordSocketClient _client;
        private static VoiceChannelController _controller;


        public ChannelListener(DiscordSocketClient client)
        {
            _client=client;
            _controller = _controller ?? new VoiceChannelController(_client);
        }
        /// <summary>
        /// Metodo que escucha el movimiento de canal, en caso de necesitar recopilar mas informacion para los procedimientos 
        /// de los cambios de canal añadirlos al "UserVoiceChannelChangeModel"
        /// </summary>
        /// <param name="user">Usuario que ha invocado el metodo</param>
        /// <param name="oldVoiceState">canal anterior</param>
        /// <param name="newVoiceState">Canal siguiente</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">Lanza la nullreferenceexception en el caso de que
        /// no haya ni canal siguiente ni anterior</exception>
        public async Task ClientUserVoiceStateUpdated(SocketUser user, SocketVoiceState oldVoiceState, SocketVoiceState newVoiceState)
        {
            UserVoiceChannelChangeModel model = new();
            model.OldVoiceState = oldVoiceState;
            model.NewVoiceState = newVoiceState;
            model.SetWorkingGuild();
            model.User = user;

            // Join
            if (oldVoiceState.VoiceChannel == null && newVoiceState.VoiceChannel != null)
            {
                _controller.UserStatusVoiceChange(VoiceChannelController.UserVoiceChange.userJoin, model);
            }
            // Move
            else if (oldVoiceState.VoiceChannel != null && newVoiceState.VoiceChannel != null)
            {
                _controller.UserStatusVoiceChange(VoiceChannelController.UserVoiceChange.userMove, model);
            }
            // Leave
            else if (oldVoiceState.VoiceChannel != null && newVoiceState.VoiceChannel == null)
            {
                _controller.UserStatusVoiceChange(VoiceChannelController.UserVoiceChange.userLeft, model);
            }
            else
            {
                throw new NullReferenceException();  
            }
        }

    }
}
