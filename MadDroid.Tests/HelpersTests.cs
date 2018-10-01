using System.Web;
using Xunit;
using MadDroid.Helpers;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System;
using System.Linq;

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

        [Fact]
        public async Task StorageTest()
        {
            var builder = new StringBuilder();
            builder.Append("this");
            builder.Append(" is");
            builder.Append(" a");
            builder.Append(" test");

            string path = "myObj.json";
            await Storage.SaveAsync(path, builder);

            var newBuilder = await Storage.ReadAsync<StringBuilder>(path);
            Assert.Equal("this is a test", newBuilder.ToString());
            File.Delete(path);
        }

        [Fact]
        public void ArrayExtensionsTest()
        {
            int[] arr = new int[849];
            var split = arr.Split(100);
            Assert.Equal(Math.Ceiling((double)849 / 100), split.ToArray().Length);
        }

        [Fact]
        public void ListExtensionsTest()
        {
            var list = Enumerable.Range(0, 849).ToList();
            var split = list.Split(100);
            Assert.Equal(Math.Ceiling((double)849 / 100), split.ToArray().Length);
        }
    }
}
