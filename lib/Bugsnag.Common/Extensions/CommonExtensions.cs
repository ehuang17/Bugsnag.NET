using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bugsnag.Common.Extensions
{
    public static class CommonExtensions
    {
        public static IEnumerable<Exception> Unwrap(this Exception ex)
        {
            if (ex == null)
            {
                return Enumerable.Empty<Exception>();
            }
            else if (ex.InnerException == null)
            {
                return new Exception[] { ex };
            }

            return new Exception[] { ex }.Concat(ex.InnerException.Unwrap());
        }

        public static IEnumerable<string> ToLines(this Exception ex)
        {
            if (ex == null || ex.StackTrace == null)
            {
                return new string[] { string.Empty };
            }

            return ex.StackTrace.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );
        }

        public static string FileParseFailureDefaultValue = "";

        public static string ParseFile(this string line)
        {
            var match = Regex.Match(line, "in (.+):line");
            if (match.Groups.Count < 2)
            {
                match = Regex.Match(line, "in (.+):[0-9]+");
                if (match.Groups.Count < 2) { return FileParseFailureDefaultValue; }
            }

            return match.Groups[1].Value;
        }

        public static string ParseMethodName(this string line)
        {
            // to extract the full method name (with namespace)
            var match = Regex.Match(line, "at ([^)]+[)])");
            if (match.Groups.Count < 2) { return line; }

            return match.Groups[1].Value;
        }

        public static int? ParseLineNumber(this string line)
        {
            var match = Regex.Match(line, ":line ([0-9]+)");
            if (match.Groups.Count < 2)
            {
                match = Regex.Match(line, ":([0-9]+)");
                if (match.Groups.Count < 2) { return null; }
            }

            return Convert.ToInt32(match.Groups[1].Value);
        }
    }
}
