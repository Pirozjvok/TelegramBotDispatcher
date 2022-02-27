using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.FSM
{
    public class Data
    {

        public DataStorage Storage { get; }

        public long User_id { get; }

        public Data(DataStorage Storage, long User_id)
        {
            this.Storage = Storage;
            this.User_id = User_id;
        }

        public async Task<object?> GetProperty(string key)
        {
            return await Storage.GetProperty(User_id, key);
        }
        public async Task<IReadOnlyDictionary<string, object>> GetProperties(params string[] keys)
        {
            return await Storage.GetProperties(User_id, keys);
        }
        public async Task<IReadOnlyDictionary<string, T>> GetProperties<T>(params string[] keys)
        {
            return await Storage.GetProperties<T>(User_id, keys);
        }

        public async Task RemoveProperties(params string[] keys)
        {
            await Storage.DeleteProperties(User_id, keys);
        }
        public async Task SetProperty(string key, object value)
        {
            await Storage.SetProperty(User_id, key, value);
        }
    }
}
