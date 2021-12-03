using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBot
{
    public class StaticUtils
    {
        public static readonly List<string> POSSIBLE_ROLES = new() { "nature", "movie", "pub", "party", "chill", "sport", "food", "games" };
        public const string SET_CHANNEL_NAME = "setup";
        public const string EVENTS_CHANNEL_NAME = "events";
        public const ulong BOT_AUTHOR_ID = 274996044809371648;
        public static SocketUser[] bannedUsers;

        public static async Task ErrorMessage(string message, ISocketMessageChannel channel)
        {
            var msg = await channel.SendMessageAsync(message);
            Thread.Sleep(5000);
            await msg.DeleteAsync();
        }
    }
}
