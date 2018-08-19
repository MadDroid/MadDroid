using System;
using System.Web;
using Xunit;
using MadDroid.Helpers;
using System.Threading.Tasks;

namespace MadDroid.Tests
{
    public class HelpersTests
    {
        [Fact]
        public void QueryStringTests()
        {
            var nvc = HttpUtility.ParseQueryString(string.Empty);
            nvc.Add("term", "matando rob�s gigantes");
            nvc.Add("country", "br");
            nvc.Add("media", "podcast");
            nvc.Add("atribute", "titleTerm");
            nvc.Add("limit", "20");

            Assert.Equal("?term=matando%20rob%C3%B4s%20gigantes&country=br&media=podcast&atribute=titleTerm&limit=20", nvc.ToQueryString());
        }

        [Fact]
        public static async Task WebTests()
        {
            string str = await Web.GetStringAsync("http://google.com");
            Assert.NotNull(str);
        }
    }
}
