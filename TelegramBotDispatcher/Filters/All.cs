using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotDispatcher.Filters;

namespace TelegramBotDispatcher.Filters
{
    public class All<T> : IFilter<T>
    {
        private IFilter<T>[] Filters { get; }
        public All(params IFilter<T>[] Filters)
        {
            this.Filters = Filters;
        }
        public bool Test(T obj)
        {
            return Filters.All(x => x.Test(obj));
        }
    }
    public class All : All<Update>
    {
        public All(params IFilter<Update>[] Filters) : base(Filters)
        {
        }
    }
}
