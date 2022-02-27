using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotDispatcher.Helpers;

namespace TelegramBotDispatcher.Filters
{
    public class TextValuesFilter : IFilter<string>, IFilter<Update>
    {
        public string[] values { get; set; }
        public IEqualityComparer<string> comparer { get; }
        public TextValuesFilter(IEqualityComparer<string> comparer, params string[] values)
        {
            this.values = values;
            this.comparer = comparer;
        }
        public TextValuesFilter(bool ignore_case, params string[] values)
        {
            this.values = values;
            if (ignore_case)
            {
                this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(StringComparison.OrdinalIgnoreCase);
            } 
            else
            {
                this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(StringComparison.Ordinal);
            }
        }

        public TextValuesFilter(params string[] values)
        {
            this.values = values;
            this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(StringComparison.OrdinalIgnoreCase);
        }
        public TextValuesFilter(StringComparison comparison, params string[] values)
        {
            this.values = values;
            this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(comparison);
        }

        public TextValuesFilter(StringComparison comparison, TextComparisonMode mode, params string[] values)
        {
            this.values = values;
            this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(comparison, mode);
        }

        public TextValuesFilter(bool ignore_case, TextComparisonMode mode, params string[] values)
        {
            this.values = values;
            if (ignore_case)
            {
                this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(StringComparison.OrdinalIgnoreCase, mode);
            }
            else
            {
                this.comparer = new TelegramBotDispatcher.Helpers.StringComparer(StringComparison.Ordinal, mode);
            }
        }
        public virtual bool Test(string message)
        {
            return values.Contains(message, comparer);
        }

        public bool Test(Update obj)
        {
            if (obj.Message?.Text == null) return false;
            return Test(obj.Message.Text);
        }
    }
}
