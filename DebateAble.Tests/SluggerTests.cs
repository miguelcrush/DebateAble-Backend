using DebateAble.Api.Services;
using Xunit;

namespace DebateAble.Tests
{
    public class SluggerTests
    {
        [Fact]
        public void Test()
        {
            var sluggerService = new SluggerService();
            var testString = "This isn't my first rodeo, champ";
            var expected = "this-isnt-my-first-rodeo-champ";

            var slugged = sluggerService.GetSlug(testString);
            Assert.True(expected == slugged);
        }
    }
}