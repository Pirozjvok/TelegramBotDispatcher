using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotDispatcher.FSM;
using TelegramBotDispatcher.Handlers;

namespace TelegramBotExample.Handlers
{
    internal class StateTest : Handler
    {
        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            if (update.Message == null) return;
            await state.SetState("cancel.waitname"); //Устанавливаем состояние
            await client.SendTextMessageAsync(update.Message.Chat.Id, "Введите имя: Для отмены нажмите (/cancel)"); //Отправляем сообщение
        }
    }

    internal class StateTest2 : Handler
    {
        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            if (update.Message?.Text == null) return;
            await data.SetProperty("name", update.Message.Text); //Устанавливаем параметр
            await state.SetState("cancel.wait2"); //Устанавливаем состояние
            await client.SendTextMessageAsync(update.Message.Chat.Id, "Введите еще что то:");
        }
    }

    internal class StateTest3 : Handler
    {
        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            if (update.Message?.Text == null) return;
            await data.SetProperty("value", update.Message.Text);
            await state.SetState("cancel.wait3");
            await client.SendTextMessageAsync(update.Message.Chat.Id, "Ну и еще что то:");
        }
    }
    internal class Result : Handler
    {
        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            if (update.Message?.Text == null) return;
            var dict = await data.GetProperties<string>("name", "value"); //Получаем словарь со значениями name,value. Обобщение<string> можно не использовать, но тогда придется в ручную приводить типы
            await state.SetState("default"); //Устанавливаем состояние по умолчанию
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Результат:");
            sb.AppendLine($"1: {dict["name"]}");
            sb.AppendLine($"2: {dict["value"]}");
            sb.AppendLine($"3: {update.Message.Text}");
            await client.SendTextMessageAsync(update.Message.Chat.Id, sb.ToString());
        }
    }
}
