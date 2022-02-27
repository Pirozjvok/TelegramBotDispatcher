using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotDispatcher.Helpers;

namespace TelegramBotDispatcher.Filters
{
    public class CommandFilter : TextValuesFilter
    {
        public string prefix { get; set; } = "/";
        public CommandFilter(params string[] names) : base(true, TextComparisonMode.Equals, names)
        {
            this.prefix = prefix;
        }
        public override bool Test(string message)
        {
            bool ret =  values.Select(x => x.StartsWith(prefix) ? x : prefix + x).Contains(message, comparer);
            return ret;
        }
    }
}
