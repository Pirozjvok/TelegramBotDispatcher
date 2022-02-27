using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBotDispatcher.Filters
{
    public enum Mode : int
    {
        Any = 0,
        All = 1  
    }
    public class RegexFilter : IFilter<string>, IFilter<Update>
    {
        public Regex[] patterns { get; set; }
        public Mode mode { get; set; }
        public RegexFilter(params Regex[] patterns)
        {
            this.patterns = patterns;
            this.mode = Mode.Any;
        }
        public RegexFilter(Mode mode, params Regex[] patterns)
        {
            this.patterns = patterns;
            this.mode = mode;
        }

        public RegexFilter(params string[] patterns)
        {
            this.patterns = Array.ConvertAll(patterns, x => new Regex(x));
            this.mode = Mode.Any;
        }
        public RegexFilter(Mode mode, params string[] patterns)
        {
            this.patterns = Array.ConvertAll(patterns, x => new Regex(x));
            this.mode = mode;
        }
        public bool Test(string message)
        {    
            switch (mode)
            {
                case Mode.Any: return patterns.Any(x => x.IsMatch(message));
                case Mode.All: return patterns.All(x => x.IsMatch(message));
                default: return patterns.Any(x => x.IsMatch(message));
            }
        }
        public bool Test(Update update)
        {
            if (update.Message?.Text == null) return false;
            return Test(update.Message.Text);
        }
    }
}
