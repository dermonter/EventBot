using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBot
{
    public  class NewEventCommand : ModuleBase<SocketCommandContext>
    {
        [Command("new")]
        [RequireOwner]
        public async Task NewEventAsync(string roleName, string eventName, string place, string date, string time, int minimalAttendees)
        {
            if (Context.Channel.Name != StaticUtils.EVENTS_CHANNEL_NAME)
            {
                return;
            }

            await Context.Message.DeleteAsync();
            if (!StaticUtils.POSSIBLE_ROLES.Contains(roleName))
            {
                await StaticUtils.ErrorMessage($"Incorrect role name: {roleName}", Context.Channel);
            }

            DateTime dateTime;
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                dateTime = DateTime.ParseExact($"{date} {time}", "dd.MM.yyyy HH:mm", provider);
            } catch (FormatException)
            {
                await StaticUtils.ErrorMessage($"Invalid time format: {date} {time}", Context.Channel);
                return;
            }

            await ReplyAsync($"{roleName}\n{eventName}\n{place}\n{date}\n{time}\n{minimalAttendees}");
        }

        [Command("new")]
        [RequireOwner]
        public async Task NewEventAsync(params string[] types)
        {
            if (Context.Channel.Name != StaticUtils.EVENTS_CHANNEL_NAME)
            {
                return;
            }

            await Context.Message.DeleteAsync();
            await StaticUtils.ErrorMessage($"Wrong number of arguments: {types.Length}/6", Context.Channel);
        }
    }
}
