using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotDispatcher.FSM;

namespace TelegramBotDispatcher.Handlers
{
    public abstract class Handler
    {
        public abstract Task Execute(ITelegramBotClient client, Update update, State state, Data data);
    }
}
