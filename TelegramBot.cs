using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TelegramBot
{
    private static TelegramBotClient _botClient;

    private static JSONParser _parser = new JSONParser();

    private static readonly string _token = _parser.ReadFile($"{AppDomain.CurrentDomain.BaseDirectory}token.txt");

    private static Admin _admin = new();
    private static User _user = new();
    private static Data _data = new();

    static void Main()
    {
        _botClient = new TelegramBotClient(_token);

        _botClient.OnMessage += OnMessageWrited;

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

    private static async Task OnMessageWrited(Telegram.Bot.Types.Message message, UpdateType type)
    {
        string? text = message.Text;

        if (_admin._adminPassword.Equals(message.Text, StringComparison.OrdinalIgnoreCase))
        {
            // Добавляем чат в список администраторских
            _admin._adminChats.TryAdd(message.Chat.Id, true);
            await _botClient.SendMessage(
                message.Chat.Id,
                "✅ Вы успешно авторизованы как администратор!",
                replyMarkup: _admin.GetAdminKeyboard()
            );
            _botClient.OnMessage += AdminPanel;
            _botClient.OnMessage -= OnMessageWrited;
            return;
        }
        switch (text)
        {
            case "/start":
                await _botClient.SendMessage(
                    message.Chat.Id,
                    Param.choice,
                    replyMarkup: _user.GetUserKeyboard()
                );
                break;
            case Param.FAQ:
                await _botClient.SendMessage(
                    message.Chat.Id,
                    "Выберите интересующий вас вопрос:",
                    replyMarkup: _user.GetFAQKeyboard() // Используем новую клавиатуру
                );
                break;

            case Param.backToMain:
                await _botClient.SendMessage(
                    message.Chat.Id,
                    Param.choice,
                    replyMarkup: _user.GetUserKeyboard() // Возвращаем основную клавиатуру
                );
                break;

            // Обработка вопросов FAQ
            case "Как сделать заказ?":
                await _botClient.SendMessage(
                    message.Chat.Id,
                    "Для оформления заказа... [текст ответа]",
                    replyMarkup: _user.GetFAQKeyboard() // Оставляем клавиатуру FAQ
                );
                break;

            case "Способы оплаты":
                await _botClient.SendMessage(
                    message.Chat.Id,
                    "Мы принимаем... [текст ответа]",
                    replyMarkup: _user.GetFAQKeyboard()
                );
                break;

            default:
                await _botClient.SendMessage(
                    message.Chat.Id,
                    "команда не зарегестрирована попробуйте еще раз!",
                    replyMarkup: _admin._adminChats[message.Chat.Id] ? _admin.GetAdminKeyboard() : _user.GetUserKeyboard()
                );
                break;
        }
        
    }

    private static async Task AdminPanel(Message message, UpdateType type)
    {
        switch (message.Text)
        {
            case Param.dopAdminPanel:
              await _botClient.SendMessage(
                  message.Chat.Id,
                  "Выберите действие:",
                  replyMarkup: _admin.GetDopAdminKeyboard()
              );
              break;
          case Param.addGorod:
              await _botClient.SendMessage(
                message.Chat.Id,
                "Выберите действие:",
                replyMarkup: _admin.GetDopAdminKeyboard()
              );
              break;
          case Param.exit:
              _admin._adminChats.TryRemove(message.Chat.Id, out _);
                 await _botClient.SendMessage(
                     message.Chat.Id,
                     "Выберите действие:",
                     replyMarkup: _user.GetUserKeyboard()
                 );
                _botClient.OnMessage -= AdminPanel;
                _botClient.OnMessage += OnMessageWrited;
                 break;  
        }
           
    }
    private static async void LoadPhotos(Message msg)
    {
        _data.photos = _data.InitializeStream();
        var photos = _data.photos.Select(x => (IAlbumInputMedia)new InputMediaPhoto(x)).ToList();
        InputMediaPhoto photo = (InputMediaPhoto)photos.First();
        photos.Remove(photos.First());
        photo.Caption = "hello";
        photos.Insert(0, photo);

        Message[] messages = await _botClient.SendMediaGroup(msg.Chat.Id, photos);
        _data.ClearData(_data.photos);

        var message1 = await _botClient.SendMessage(msg.Chat.Id, "messages", replyMarkup: new string[][]
        {
            ["Help me"],
            ["Call me ☎️", "Write me ✉️"]
        });
    }
}
