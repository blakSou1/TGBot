using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class TelegramBot
{
    private readonly TelegramBotClient _botClient;
    private readonly string _token = "TOKEN";

    private Serializer serializer = new();

    public TelegramBot()
    {
        _botClient = new TelegramBotClient(_token);
        serializer.Init();
    }

    public async Task StartAsync()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { } // Получаем все типы обновлений
        };

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions
        );

        var me = await _botClient.GetMe();
        Console.WriteLine($"Бот {me.Username} запущен!");
    }

    private async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
