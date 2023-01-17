using Discord;
using Discord.WebSocket;
using System;
using System.ComponentModel;
using Yui.Models;

namespace Yui.Events.Controllers
{
    /// <summary>
    /// Clase encargada de la seleccion de logica de los movimientos entre canales
    /// </summary>
    public class VoiceChannelController
    {   
        /// <summary>
        /// Cliente nuevo
        /// </summary>
        private readonly DiscordSocketClient _client;
        /// <summary>
        /// Enum en el que se guardan las opciones del switch de gestion de canal, en caso de querer añadir ej "userGuildChange" añadirlo tambien al enum para mantener la coexion del codigo
        /// </summary>
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
        /// <summary>
        /// Metodo encargado de seleccionar el tipo de movimiento de usuario y enviarlo al metodo de logica correspondiente 
        /// </summary>
        /// <param name="option">Tipo de movimiento de canal, establecidos en el enum "UserVoiceChange"</param>
        /// <param name="uvccm">Datos necesarios para gestionar las opciones de logica de los cambios dew canal</param>
        /// <exception cref="InvalidEnumArgumentException">En caso de no corresponder a ningun valor del switch del enum mencionado, devuelve esta excepcion</exception>
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
        /// <summary>
        /// Metodo lanzado en caso de que el usuario se mueva
        /// </summary>
        private void userMove()
        {

        }
        /// <summary>
        /// Metodo lanzado en caso de que el usuario entre en un canal sin estar previamente en uno
        /// </summary>
        private void userJoin()
        {

        }
        /// <summary>
        /// Metodo lanzado cuando el usuario accede a un canal en entrar posteriormente a otro
        /// </summary>
        private void userLeft()
        {

        }


    }
}
