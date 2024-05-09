using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System.Threading.Tasks;

namespace OMNI.Commands
{
    [SlashCommandGroup("omni", "All of OMNI bot's commands.")]
    public class OmniCommands : ApplicationCommandModule
    {
        [SlashCommand("setup", "[ADMIN ONLY] Setup the ticketing system.")]
        [SlashRequirePermissions(Permissions.Administrator)]
        public async Task SetupTicketingSystem(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Hello world!"));
        }
    }
}