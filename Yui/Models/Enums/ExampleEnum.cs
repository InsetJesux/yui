using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yui
{
    public enum ExampleEnum
    {
        First,
        Second,
        Third,
        Fourth,
        [ChoiceDisplay("Twenty First")]
        TwentyFirst
    }
}
