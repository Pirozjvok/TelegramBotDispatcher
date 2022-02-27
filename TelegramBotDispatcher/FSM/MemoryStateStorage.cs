using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.FSM
{
    public class MemoryStateStorage : StateStorage
    {
        public ConcurrentDictionary<long, string> Storage { get; set; } = new ConcurrentDictionary<long, string>();
        public override Task<string> GetState(long user_id)
        {
            if (Storage.TryGetValue(user_id, out var value))
            {
                return Task.FromResult(value);
            }
            else
            {
                return Task.FromResult("default");
            }
        }

        public override Task SetState(long user_id, string state)
        {
            Storage[user_id] = state;
            return Task.CompletedTask;
        }
    }
}
