using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBot
{
    public class ReactionHandler
    {
        private const string CHANNEL_NAME = "events";
        private const string ANNOUNCEMENTS_CHANNEL_NAME = "announcements";
        private readonly DiscordSocketClient client;

        public ReactionHandler(DiscordSocketClient client)
        {
            this.client = client;
        }

        public async Task HandleAddReactionAsync(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (channel.Name != CHANNEL_NAME)
            {
                return;
            }

            var msg = await message.GetOrDownloadAsync();

            if (msg.Author.Id != client.CurrentUser.Id)
            {
                return;
            }

            if (reaction.Emote.Name != "👍")
            {
                await msg.RemoveReactionAsync(reaction.Emote, reaction.UserId);
                return;
            }

            var messageParsed = msg.Content.Split('\n');

            if (msg.Reactions.Count == int.Parse(messageParsed.Last()))
            {
                // Announce the cool message (and clean up afterwards maybe?)
                var chnl = channel as SocketGuildChannel;
                SocketRole role = chnl.Guild.Roles.First(x => x.Name == messageParsed[0]);
                var announcementChannel = chnl.Guild.Channels.FirstOrDefault(x => x.Name == ANNOUNCEMENTS_CHANNEL_NAME) as SocketTextChannel;
                await announcementChannel.SendMessageAsync(
                    $"{role.Mention} Listen up everyone we have a new event for you\nWhat: {messageParsed[1]}\nWhere: {messageParsed[2]}\nWhen: {messageParsed[3]} at {messageParsed[4]}");
                await msg.DeleteAsync();
            }
        }
    }
}
