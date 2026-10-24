using Telegram.Bot;

namespace KnewAlreadyTelegramBot;

public class KnewAlreadyTelegramBotClient
{
    private readonly TelegramBotClient client;

    public KnewAlreadyTelegramBotClient(string apiKey)
    {
        client = new TelegramBotClient(apiKey);
    }

    public async Task NotifyBotUser(string userTelegramName, string message)
    {
        var updates = await client.GetUpdatesAsync();

        var chat = updates.FirstOrDefault(u => u.Message?.Chat?.Username == userTelegramName)?.Message.Chat;
        if (chat != null)
        {
            await client.SendTextMessageAsync(chat.Id, message);
        }
        //var data = await client.GetChatAsync();
        //await client.SendTextMessageAsync();
    }
}