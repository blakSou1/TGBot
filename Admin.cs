using System.Collections.Concurrent;
using Telegram.Bot.Types.ReplyMarkups;

public class Admin
{
    public readonly string _adminPassword = "Green!";
    public ConcurrentDictionary<long, bool> _adminChats = new();

    // Метод для создания клавиатуры администратора
    public ReplyKeyboardMarkup GetAdminKeyboard()
    {
        return new ReplyKeyboardMarkup(
        [
            ["➕ Добавить объявление", "✏️ Редактировать объявление"],
            ["❌ Удалить объявление", "📋 Список объявлений"],
            ["🚪 Выйти"]
        ])
        {
            ResizeKeyboard = true
        };
    }

    // Метод для удаления прав администратора
    public void RemoveAdminAccess(long chatId)
    {
        _adminChats.TryRemove(chatId, out _);
    }
}