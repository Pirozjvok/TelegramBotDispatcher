using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.Helpers
{
    public enum TextComparisonMode : int
    {
        Equals = 0,
        Contains = 1,
        StartsWith = 2,
        EndsWith = 3,
    }
    public class StringComparer : IEqualityComparer<string>
    {
        private StringComparison comparison { get; }
        private TextComparisonMode ComparisonMode { get; }
        public StringComparer(StringComparison comparison, TextComparisonMode mode = TextComparisonMode.Equals)
        {
            this.comparison = comparison;
            this.ComparisonMode = mode;
        }
        public bool Equals(string? x, string? y)
        {
            if (x == null || y == null) return false;
            switch (ComparisonMode)
            {
                case TextComparisonMode.Equals: return x.Equals(y, comparison);
                case TextComparisonMode.Contains: return x.Contains(y, comparison);
                case TextComparisonMode.StartsWith : return x.StartsWith(y, comparison);
                case TextComparisonMode.EndsWith : return x.EndsWith(y, comparison);
                default: return x.Equals(y, comparison);
            }
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
