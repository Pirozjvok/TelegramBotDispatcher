using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotDispatcher.Filters
{
    public class UpdateTypeFilter : IFilter<Update>
    {
        public UpdateType[] values { get; set; }
        public UpdateTypeFilter(params UpdateType[] values)
        {
            this.values = values;
        }
        public bool Test(Update update)
        {
            return values.Contains(update.Type);
        }
    }
}
