using System;
using System.Web;
using Xunit;
using MadDroid.Helpers;
using System.Threading.Tasks;
using System.Text;

namespace MadDroid.Tests
{
    public class HelpersTests
    {
        [Fact]
        public void QueryStringTests()
        {
            var nvc = HttpUtility.ParseQueryString(string.Empty);
            nvc.Add("term", "matando robôs gigantes");
            nvc.Add("country", "br");
            nvc.Add("media", "podcast");
            nvc.Add("atribute", "titleTerm");
            nvc.Add("limit", "20");

            Assert.Equal("?term=matando%20rob%C3%B4s%20gigantes&country=br&media=podcast&atribute=titleTerm&limit=20", nvc.ToQueryString());
        }

        [Fact]
        public async Task WebTests()
        {
            string str = await Web.GetStringAsync("http://google.com");
            Assert.NotNull(str);
        }

        [Fact]
        public void SingletonTests()
        {
            Singleton<StringBuilder>.Instance.Append("this");
            Singleton<StringBuilder>.Instance.Append(" is");
            Singleton<StringBuilder>.Instance.Append(" a");
            Singleton<StringBuilder>.Instance.Append(" test");
            Assert.Equal("this is a test", Singleton<StringBuilder>.Instance.ToString());
        }
    }
}
