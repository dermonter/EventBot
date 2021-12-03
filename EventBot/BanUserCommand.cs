using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace EventBot
{
    public class BanUserCommand : ModuleBase<SocketCommandContext>
    {   
        [Command("ban")]
        [RequireOwner]
        public async Task NewEventAsync(params SocketUser[] users) {
            if (Context.Channel.Name != StaticUtils.SET_CHANNEL_NAME)
            {
                return;
            }

            StaticUtils.bannedUsers = users;

            await Context.Message.DeleteAsync();
        }

        [Command("ban")]
        [RequireOwner]
        public async Task NewEventAsync(params string[] users)
        {
            if (Context.Channel.Name != StaticUtils.SET_CHANNEL_NAME)
            {
                return;
            }

            await Context.Message.DeleteAsync();

            await StaticUtils.ErrorMessage($"Bad user name", Context.Channel);
        }
    }
}
