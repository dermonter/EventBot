using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBot
{
    public class SetInterestsCommand : ModuleBase<SocketCommandContext>
    {
        [Command("set")]
        [RequireOwner]
        public async Task SetInterestsAsync(params string[] roles)
        {
            if (Context.Channel.Name != StaticUtils.SET_CHANNEL_NAME)
            {
                return;
            }

            await Context.Message.DeleteAsync();

            var user = Context.User;
            Func<IEnumerable<string>, IEnumerable<SocketRole>> stringToRole = list => list.Select(role => Context.Guild.Roles.FirstOrDefault(x => x.Name == role));
            if (roles.Length == 0)
            {
                await (user as IGuildUser).AddRolesAsync(stringToRole(StaticUtils.POSSIBLE_ROLES));
                return;
            }

            foreach (var role in roles)
            {
                if (!StaticUtils.POSSIBLE_ROLES.Contains(role))
                {
                    await StaticUtils.ErrorMessage($"Bad role: {role}", Context.Channel);
                    return;
                }
            }
            List<string> rolesList = new List<string>(roles);

            await (user as IGuildUser).RemoveRolesAsync(stringToRole(StaticUtils.POSSIBLE_ROLES));
            await (user as IGuildUser).AddRolesAsync(stringToRole(rolesList));
        }
    }
}
