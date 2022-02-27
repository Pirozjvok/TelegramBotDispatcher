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
    class SetState : Handler
    {
        public string State { get; set; }
        public string? replyMessage { get; set; }
        public SetState(string State, string? replyMessage = null)
        {
            this.State = State;
            this.replyMessage = replyMessage;
        }
        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            await state.SetState(State);
            if (replyMessage != null && update.Message != null) await client.SendTextMessageAsync(update.Message.Chat.Id, replyMessage);
        }
    }

}
