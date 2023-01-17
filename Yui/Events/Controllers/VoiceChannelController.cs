using Discord;
using Discord.WebSocket;
using System;
using System.ComponentModel;
using Yui.Models;

namespace Yui.Events.Controllers
{
    public class VoiceChannelController
    {
        private readonly DiscordSocketClient _client;
        public enum UserVoiceChange
        {
            userMove,
            userLeft,
            userJoin
        }
        public VoiceChannelController(DiscordSocketClient client)
        {
            _client= client;
        }
        public void UserStatusVoiceChange(VoiceChannelController.UserVoiceChange option, UserVoiceChannelChangeModel uvccm)
        {
            Console.WriteLine("Valor: "+option+ "\r\nGuild: "+uvccm.Guild.Name);
            Console.WriteLine("User: "+uvccm.User);

            switch (option)
            {
                case UserVoiceChange.userMove:
                    break;
                case UserVoiceChange.userLeft:
                    break;
                case UserVoiceChange.userJoin:
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
        private void userMove()
        {

        }
        private void userJoin()
        {

        }
        private void userLeft()
        {

        }


    }
}
