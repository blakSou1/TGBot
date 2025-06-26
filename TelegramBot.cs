using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TelegramBot
{
    private static TelegramBotClient _botClient;
    private static readonly string _token = "8034068836:AAH-xrKPOaAD1NF_pymDj0TWn-1VeQHE0tg";

    private static Serializer serializer = new();
    private static Admin admin = new();

    static void Main()
    {
        _botClient = new TelegramBotClient(_token);
        serializer.Init();
        _ = StartAsync();

        Console.WriteLine("Нажмите Enter для завершения");
        Console.ReadLine();
    }

    public static async Task StartAsync()
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

    private static async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        await Task.CompletedTask;
    }

    /// <summary>
    /// Обработка входящих сообщений
    /// </summary>
    /// <param name="client"></param>
    /// <param name="update"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message != null)
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
        await Task.CompletedTask;
    }
    private static async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (message?.Text == null) return;

        long chatId = message.Chat.Id;

        // Проверка на пароль администратора
        if (message.Text == admin._adminPassword)
        {
            // Добавляем чат в список администраторских
            admin._adminChats.TryAdd(chatId, true);

            await _botClient.SendMessage(
                chatId,
                "✅ Вы успешно авторизованы как администратор!",
                cancellationToken: cancellationToken,
                replyMarkup: admin.GetAdminKeyboard()
            );
            return;
        }

        switch (message.Text)
        {
            case "/start":
                break;
            case "/menu":
                break;
            default:
                await _botClient.SendMessage(
                    chatId,
                    "команда не зарегестрирована попробуйте еще раз!",
                    cancellationToken: cancellationToken
                );
                break;
        }
    }
}
