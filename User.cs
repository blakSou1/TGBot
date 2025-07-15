using Telegram.Bot.Types.ReplyMarkups;

public class User
{
    
public ReplyKeyboardMarkup GetUserKeyboard()
    {
        return new ReplyKeyboardMarkup(
        [
            ["📢 Объявление", "❓ FAQ"]
        ])
        {
            ResizeKeyboard = true
        };
    }
}