using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBot
{
    public class AutoAssignEvent
    {
        public async Task HandleMessageAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!message.Author.IsBot)
                return;

            var content = message.Content.Split('\n');

            if (content.Length != 6)
                return;

            var authorUser = await messageParam.Channel.GetUserAsync(StaticUtils.BOT_AUTHOR_ID) as SocketGuildUser;
            var authorRoles = authorUser.Roles.Select(r => r.Name);
            if (StaticUtils.bannedUsers.Select(u => u.Id).Contains(message.Author.Id))
                return;
            if (authorRoles.Contains(content[0]))
            {
                await message.AddReactionAsync(new Emoji("👍"));
            }
        }
    }
}
