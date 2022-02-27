using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.FSM
{
    //Кароче вот так вот нужно это ну типо тоси боси чтоб потокобезопасная вся эта хуйня была
    public abstract class DataStorage
    {
        public abstract Task<object?> GetProperty(long user_id, string key);
        public abstract Task SetProperty(long user_id, string key, object value, bool serialize = false);
        public abstract Task<string[]> GetKeys(long user_id);
        public abstract Task<IReadOnlyDictionary<string, object>> GetProperties(long user_id, params string[] keys);
        public virtual async Task<IReadOnlyDictionary<string, T>> GetProperties<T>(long user_id, params string[] keys)
        {
            return (await GetProperties(user_id, keys)).Where(x => x.Value is T).ToDictionary(x => x.Key, x => (T)x.Value);
        }
        public abstract Task DeleteProperties(long user_id, params string[] keys);
    }
}
