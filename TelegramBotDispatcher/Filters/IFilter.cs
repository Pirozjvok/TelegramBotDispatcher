using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotDispatcher.Filters
{
    public interface IFilter<T>
    {
        bool Test(T obj);
    }
}
