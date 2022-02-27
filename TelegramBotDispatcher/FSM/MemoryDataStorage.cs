using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.FSM
{
    public class MemoryDataStorage : DataStorage
    {
        public ConcurrentDictionary<long, ConcurrentDictionary<string, object>> Datas { get; set; } = new ConcurrentDictionary<long, ConcurrentDictionary<string, object>>();
        public override Task DeleteProperties(long user_id, params string[] keys)
        {
            if (Datas.ContainsKey(user_id))
            {
                ConcurrentDictionary<string, object> Properties = Datas[user_id];
                Array.ForEach(keys, x => Properties.TryRemove(x, out object? value));
            }
            return Task.CompletedTask;
        }

        public override Task<string[]> GetKeys(long user_id)
        {
            if (Datas.ContainsKey(user_id))
            {
                ConcurrentDictionary<string, object> Properties = Datas[user_id];
                return Task.FromResult(Properties.Select(x => x.Key).ToArray());
            }
            else
            {
                return Task.FromResult(Array.Empty<string>());
            }
        }

        public override Task<IReadOnlyDictionary<string, object>> GetProperties(long user_id, params string[] keys)
        {
            if (Datas.ContainsKey(user_id))
            {
                ConcurrentDictionary<string, object> Properties = Datas[user_id];
                var Filtered = Properties.Where(x => keys.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                if (Filtered != null) return Task.FromResult<IReadOnlyDictionary<string, object>>(Filtered);
            }              
            return Task.FromResult<IReadOnlyDictionary<string, object>>(new Dictionary<string, object>());
        }

        public override Task<object?> GetProperty(long user_id, string key)
        {
            if (Datas.ContainsKey(user_id))
            {
                ConcurrentDictionary<string, object> Properties = Datas[user_id];
                if (Properties.TryGetValue(key, out object? value))
                {
                    return Task.FromResult<object?>(value);
                } 
                else
                {
                    return Task.FromResult<object?>(null);
                }
            }
            else
            {
                return Task.FromResult<object?>(null);
            }
        }

        public override Task SetProperty(long user_id, string key, object value, bool serialize)
        {
            if (!Datas.ContainsKey(user_id)) Datas.TryAdd(user_id, new ConcurrentDictionary<string, object>());
            ConcurrentDictionary<string, object> Properties = Datas[user_id];
            Properties[key] = value;
            return Task.CompletedTask;
        }
    }
}
