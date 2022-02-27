using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotDispatcher.Filters;
using TelegramBotDispatcher.Handlers;

namespace TelegramBotDispatcher.FSM
{
    //TODO: Короче нужно использовать несколько сторагов для разных ситуаций
    //Например для сообщений одно а для вступившего в группу другую
    public abstract class StateStorage
    {
        public abstract Task<string> GetState(long user_id);
        public abstract Task SetState(long user_id, string state);
    }
}
