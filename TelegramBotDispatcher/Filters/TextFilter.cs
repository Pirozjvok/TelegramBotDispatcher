using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotDispatcher.Filters
{
    public class TextFilter : IFilter<Update>
    {
        private IFilter<string> _filter;
        public TextFilter(IFilter<string> filter)
        {
            _filter = filter;
        }
        public bool Test(Update update)
        {
            if(update.Message?.Text == null) return false;
            return _filter.Test(update.Message.Text);
        }
    }
}
