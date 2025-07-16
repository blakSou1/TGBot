using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class TelegramBot
{
    private static TelegramBotClient _botClient;
    private static readonly string _token = "8034068836:AAH-xrKPOaAD1NF_pymDj0TWn-1VeQHE0tg";
    
    private static Admin admin = new();
    private static User user = new();
    private static Data data = new();

    static void Main()
    {
        _botClient = new TelegramBotClient(_token);
        _ = StartAsync();

        while (true)
        {
            Console.WriteLine("Press any key to exit...");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                System.Environment.Exit(0);
            }
        }
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
        if (update.Message is null) return;
        switch (update.Type)
        {
            case UpdateType.Message:
                await HandleMessageAsync(client, update.Message, token);
                break;
            case UpdateType.CallbackQuery:
                //await HandleCallbackQueryAsync(client, update.CallbackQuery, token);
                break;
        }
        
        await Task.CompletedTask;
    }
    private static async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        long chatId = message.Chat.Id;

        data.photos = data.InitializeStream();
        var photos = data.photos.Select(x =>  (IAlbumInputMedia)new InputMediaPhoto(x)).ToList();
        InputMediaPhoto photo = (InputMediaPhoto)photos.First();
        photos.Remove(photos.First());
        photo.Caption = "hello";
        photos.Insert(0, photo);

        Message[] messages = await botClient.SendMediaGroup(chatId, photos);
        data.ClearData(data.photos);
        
        var message1 = await botClient.SendMessage(chatId, "messages", replyMarkup: new string[][]
        {
            ["Help me"],
            ["Call me ☎️", "Write me ✉️"]
        });
        
        // Проверка на пароль администратора
        if (admin._adminPassword.Equals(message.Text,  StringComparison.OrdinalIgnoreCase))
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
                await _botClient.SendMessage(
                    chatId,
                    Param.choice,
                    cancellationToken: cancellationToken,
                    replyMarkup: user.GetUserKeyboard()
                );
                break;

            #region User
            case Param.announcement:
                await _botClient.SendMessage(
                    chatId,
                    Param.gorod,
                    cancellationToken: cancellationToken,
                    replyMarkup: user.GetUserKeyboardGorod()
                );
                break;

            case Param.FAQ:
                await _botClient.SendMessage(
                    chatId,
                    "Выберите действие:",
                    cancellationToken: cancellationToken,
                    replyMarkup: user.GetUserKeyboard()
                );
                break;
            #endregion

            #region Admin
            case Param.dopAdminPanel:
                await _botClient.SendMessage(
                    chatId,
                    "Выберите действие:",
                    cancellationToken: cancellationToken,
                    replyMarkup: admin.GetDopAdminKeyboard()
                );
                break;
            case Param.addGorod:
                await _botClient.SendMessage(
                    chatId,
                    "Выберите действие:",
                    cancellationToken: cancellationToken
                    //replyMarkup: admin.GetDopAdminKeyboard()
                );
                break;
            case Param.exit:
                admin._adminChats.TryRemove(chatId, out _);

                await _botClient.SendMessage(
                    chatId,
                    "Выберите действие:",
                    cancellationToken: cancellationToken,
                    replyMarkup: user.GetUserKeyboard()
                );
                break;

            #endregion

            case "/menu":
                break;

            default:
                await _botClient.SendMessage(
                    chatId,
                    "команда не зарегестрирована попробуйте еще раз!",
                    cancellationToken: cancellationToken,
                    replyMarkup: admin._adminChats[chatId]? admin.GetAdminKeyboard() : user.GetUserKeyboard()
                );
                break;
        }
    }
    
}
