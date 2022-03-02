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
        public virtual async Task<IReadOnlyDictionary<string, object>> PopProperties(long user_id, params string[] keys)
        {
            IReadOnlyDictionary<string, object> valuePairs = await GetProperties(user_id, keys);
            await DeleteProperties(user_id, valuePairs.Keys.ToArray());
            return valuePairs;
        }
        public virtual async Task<IReadOnlyDictionary<string, T>> PopProperties<T>(long user_id, params string[] keys)
        {
            IReadOnlyDictionary<string, T> valuePairs = await GetProperties<T>(user_id, keys);
            await DeleteProperties(user_id, valuePairs.Keys.ToArray());
            return valuePairs;
        }
        public virtual async Task<object?> PopProperty(long user_id, string key)
        {
            object? value = await GetProperty(user_id, key);
            await DeleteProperties(user_id, key);
            return value;
        }
        public abstract Task DeleteProperties(long user_id, params string[] keys);
    }
}
