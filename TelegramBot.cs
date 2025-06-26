using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
        Console.WriteLine($"Ошибка: {exception.Message}");
    }

    /// <summary>
    /// Обработка входящих сообщений
    /// </summary>
    /// <param name="client"></param>
    /// <param name="update"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await HandleMessageAsync(client, update.Message, token);
                break;
            case UpdateType.CallbackQuery:
                //await HandleCallbackQueryAsync(client, update.CallbackQuery, token);
                break;
        }
    }
    private async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (message?.Text == null) return;

        long chatId = message.Chat.Id;

        switch (message.Text.ToLower())
        {
            case "/start":
                break;
            case "/menu":
                break;
            default:
                break;
        }
    }
}
