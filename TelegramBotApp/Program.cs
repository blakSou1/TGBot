using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

class Program // классы
{
  static async Task Main(string[] args)//методы
  {
      // Вставьте токен от @BotFather
      var botClient = new TelegramBotClient("ВАШ_ТОКЕН");

      var cts = new CancellationTokenSource();
      var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

      botClient.StartReceiving(
          HandleUpdateAsync, 
          HandleErrorAsync, 
          receiverOptions, 
          cts.Token
      );

      var me = await botClient.GetMeAsync();
      Console.WriteLine($"Бот {me.Username} запущен!");

      Console.ReadLine();
      cts.Cancel();
  }

  static async Task HandleUpdateAsync(
      ITelegramBotClient botClient, 
      Update update, 
      CancellationToken cancellationToken)
  {
      if (update.Message?.Text != null)
      {
          var message = update.Message;
          Console.WriteLine($"Получено: {message.Text}");

          await botClient.SendTextMessageAsync(
              chatId: message.Chat.Id,
              text: $"Вы написали: {message.Text}",
              cancellationToken: cancellationToken
          );
      }
  }

  static Task HandleErrorAsync(
      ITelegramBotClient botClient, 
      Exception exception, 
      CancellationToken cancellationToken)
  {
      Console.WriteLine($"Ошибка: {exception.Message}");
      return Task.CompletedTask;
  }
}