using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yui.Models
{
    public class UserVoiceChannelChangeModel
    {
        public IGuild Guild { get; set; }
        [AllowNull] public SocketVoiceState OldVoiceState { get; set; }
        [AllowNull] public SocketVoiceState NewVoiceState { get; set; }
        public SocketUser User { get; set; }

        /// <summary>
        /// Revisa los dos socketvoicestate para identificar sobre que Guild esta trabajando
        /// </summary>
        public void SetWorkingGuild()
        {
            if (this.OldVoiceState.VoiceChannel != null)
            {
                Guild = OldVoiceState.VoiceChannel.Guild;
            }
            else if(this.NewVoiceState.VoiceChannel != null)
            {
                Guild= NewVoiceState.VoiceChannel.Guild;    
            }
            else
            {
                Guild = null;
            }
        }

    }
}
