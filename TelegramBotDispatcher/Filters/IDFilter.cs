using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotDispatcher.Filters
{
    public enum IDFilterMode : int
    {
        WhiteListMode = 0,
        BlackListMode = 1,
    }
    public class IDFilter : IFilter<Update>
    {
        public IEnumerable<long> ids { get; set; }
        public IDFilterMode filterMode { get; set;}
        public IDFilter(IDFilterMode filterMode, params long[] ids)
        {
            this.ids = ids;
            this.filterMode = filterMode;  
        }
        public IDFilter(params long[] ids)
        {
            this.ids = ids;
            this.filterMode = IDFilterMode.WhiteListMode;
        }
        public IDFilter(IEnumerable<long> ids, IDFilterMode filterMode = IDFilterMode.WhiteListMode)
        {
            this.ids = ids;
            this.filterMode = filterMode;
        }
        public bool Test(Update update)
        {
            if (update.Message?.Chat?.Id == null) return false;
            if (filterMode == IDFilterMode.WhiteListMode)
            {
                 return ids.Contains(update.Message.Chat.Id);
            } 
            else if (filterMode == IDFilterMode.BlackListMode)
            {
                return !ids.Contains(update.Message.Chat.Id);
            } 
            else
            {
                return ids.Contains(update.Message.Chat.Id);
            }
            
        }
    }
}
