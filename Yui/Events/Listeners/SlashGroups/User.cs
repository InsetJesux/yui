using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yui.Events.Listeners.SlashGroups
{
    [Group("test_group", "This is a command group")]
    public class User : InteractionModuleBase<SocketInteractionContext>
    {
         // Dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
        public InteractionService Commands { get; set; }

        private InteractionHandler _handler;
        public User(InteractionHandler handler) 
        {
            _handler = handler;
        }
        // You can create command choices either by using the [Choice] attribute or by creating an enum. Every enum with 25 or less values will be registered as a multiple
        // choice option
       [SlashCommand("choice_example", "Enums create choices")]
        public async Task ChoiceExample(ExampleEnum input)
            => await RespondAsync(input.ToString()+" - Hola desde la clase User Group");
    }
}
