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
    internal class Help : Handler
    {
        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            if (update.Message == null) return;
            await client.SendTextMessageAsync(update.Message.Chat.Id, "Hello World!\nТест: /go");
        }
    }
}
