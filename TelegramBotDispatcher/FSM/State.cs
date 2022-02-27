using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.FSM
{
    public class State
    {
        public StateStorage Storage { get; }
        public long User_id { get; }
        public State(StateStorage storage, long user_id)
        {
            this.Storage = storage;
            this.User_id = user_id;
        }

        public async Task<string> GetState()
        {
            return await Storage.GetState(User_id);
        }

        public async Task SetState(string state)
        {
            await Storage.SetState(User_id, state);
        }
    }
}
