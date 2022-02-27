using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotDispatcher.Filters;

namespace TelegramBotDispatcher.Filters
{
    public class Any<T> : IFilter<T>
    {
        private IFilter<T>[] Filters { get; }
        public Any(params IFilter<T>[] Filters)
        {
            this.Filters = Filters;
        }
        public bool Test(T obj)
        {
            return Filters.Any(x => x.Test(obj));
        }
    }
    public class Any : Any<Update>
    {
        public Any(params IFilter<Update>[] Filters) : base(Filters)
        {
        }
    }
}
