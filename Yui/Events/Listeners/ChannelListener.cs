using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Yui.Events.Controllers;
using Yui.Models;

namespace Yui.Events.Listeners
{
    public sealed class ChannelListener
    {
        private readonly DiscordSocketClient _client;
        private static VoiceChannelController _controller;


        public ChannelListener(DiscordSocketClient client)
        {
            _client=client;
            _controller = _controller ?? new VoiceChannelController(_client);
        }
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
