using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotDispatcher.Filters
{
    public class CustomTextFilter : IFilter<string>, IFilter<Update>
    {
        private Func<string, bool> _filter { get; set; }
        public CustomTextFilter(Func<string, bool> filter)
        {
            _filter = filter;
        }
        public bool Test(string text)
        {
            return _filter(text);
        }

        public bool Test(Update obj)
        {
            if (obj.Message?.Text == null) return false;
            return Test(obj.Message.Text);
        }
    }
}
