using DSharpPlus;
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
            /*await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            var openTicket = new DiscordButtonComponent(ButtonStyle.Primary, "create", "Create Support Thread");
            var closeTicket = new DiscordButtonComponent(ButtonStyle.Danger, "close", "Close Existing Thread");

            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddComponents(openTicket, closeTicket).AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.CornflowerBlue)
                .WithTitle("❓ Support Ticket")
                .WithDescription("Click below to start a new support thread!")));*/
        }
    }
}
