using System.Text.RegularExpressions;

namespace DebateAble.Api.Services
{
    public interface ISluggerService
    {
        public string GetSlug(string text);
    }
    public class SluggerService : ISluggerService
    {
        private static Regex _spaceRegex = new Regex(@"\s");
        private static Regex _quoteRegex = new Regex(@"'|""");
        private static Regex _nonWordRegex = new Regex(@"\W");
        private static Regex _multipleHyphenRegex = new Regex(@"-{2,}");

        public SluggerService(
            
            )
        {

        }

        public string GetSlug(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var toSlug = text.ToLower();
            toSlug = _spaceRegex.Replace(toSlug, "-");
            toSlug = _quoteRegex.Replace(toSlug, "");
            toSlug = _nonWordRegex.Replace(toSlug, "-");
            toSlug = _multipleHyphenRegex.Replace(toSlug, "-");

            return toSlug;
        }
    }
}
