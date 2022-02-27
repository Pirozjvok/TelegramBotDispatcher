using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotDispatcher.Filters
{
    public class CustomUpdateFilter : IFilter<Update>
    {
        private Func<Update, bool> filter { get; set; }
        public CustomUpdateFilter(Func<Update, bool> filter)
        {
            this.filter = filter;
        }
        public bool Test(Update update)
        {
            return filter(update);
        }
    }
}
