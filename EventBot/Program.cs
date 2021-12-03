using bot.Handlers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using EventBot;
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main()
    {
        var client = new DiscordSocketClient();
        var commandService = new CommandService();
        var reactionHandler = new ReactionHandler(client);
        var autoAssignEvent = new AutoAssignEvent();

        // Log information to the console
        client.Log += Log;
        client.ReactionAdded += reactionHandler.HandleAddReactionAsync;
        client.MessageReceived += autoAssignEvent.HandleMessageAsync;

        // Read the token for your bot from file
        var token = File.ReadAllText("token.txt");

        // Log in to Discord
        await client.LoginAsync(TokenType.Bot, token);

        // Start connection logic
        await client.StartAsync();

        // Here you can set up your event handlers
        var commandHandler = new CommandHandler(client, commandService);
        await commandHandler.SetupAsync();

        // Block this task until the program is closed
        await Task.Delay(-1);
    }

    private static Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}