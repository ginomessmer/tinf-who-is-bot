using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using FluentValidation.Results;

namespace TinfWhoIs.DiscordBot.Internal
{
    public static class DiscordEmbedHelpers
    {
        public static Embed ToEmbed(this ValidationResult result) => new EmbedBuilder()
            .WithTitle("Ungültige Eingabe")
            .WithDescription(result.ToString())
            .WithColor(Color.Red)
            .WithFooter("Probiere es erneut sobald du die Fehler behoben hast.")
            .Build();
    }
}
