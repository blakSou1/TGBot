using Telegram.Bot.Types.ReplyMarkups;

public class User
{
    public ReplyKeyboardMarkup GetUserKeyboard()
    {
        return new ReplyKeyboardMarkup(
        [
            [Param.announcement, Param.FAQ]
        ])
        {
            ResizeKeyboard = true
        };
    }
    public ReplyKeyboardMarkup GetUserKeyboardGorod()
    {
        var keyboard = new List<KeyboardButton[]>();
        var row = new List<KeyboardButton>();

        foreach (var text in Serializer.gorods)
        {
            row.Add(new KeyboardButton(text));
        }

        keyboard.Add([.. row]);

        return new ReplyKeyboardMarkup(keyboard)
        {
            ResizeKeyboard = true
        };

        // return new ReplyKeyboardMarkup(
        // [
        //     [Param.announcement, Param.FAQ]
        // ])
        // {
        //     ResizeKeyboard = true
        // };
    }

}