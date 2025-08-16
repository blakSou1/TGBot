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
    
    // Новый метод для клавиатуры FAQ
    public ReplyKeyboardMarkup GetFAQKeyboard()
    {
        return new ReplyKeyboardMarkup(
        [
            ["Как сделать заказ?", "Способы оплаты"],
            ["Доставка и самовывоз", "Гарантии"],
            [Param.backToMain] // Кнопка возврата в главное меню
        ])
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true // Клавиатура скроется после выбора
        };
    }
}