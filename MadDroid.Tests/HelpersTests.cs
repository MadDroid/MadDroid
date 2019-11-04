using System.Web;
using Xunit;
using MadDroid.Helpers;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System;
using System.Linq;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Threading;

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
            string path = "myObj.json";

            var builder = new StringBuilder();
            builder.Append("this");
            builder.Append(" is");
            builder.Append(" a");
            builder.Append(" test");

            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            var formatting = Newtonsoft.Json.Formatting.Indented;

            var value = builder.ToString();

            await Assert.ThrowsAnyAsync<Exception>(() => Storage.SaveAsync(null, builder));

            await Storage.SaveAsync(path, builder, formatting, settings); ;

            Assert.True(File.Exists(path));

            await Assert.ThrowsAnyAsync<Exception>(() => Storage.ReadAsync<StringBuilder>(null, settings));

            var result = await Storage.ReadAsync<StringBuilder>(path, settings);

            Assert.NotNull(result);
            Assert.Equal(value, result.ToString());

            File.Delete(path);

            var saved = await Storage.TrySaveAsync(null, builder);

            Assert.False(saved);

            saved = await Storage.TrySaveAsync(path, builder, formatting, settings);

            Assert.True(saved);

            result = await Storage.TryReadAsync<StringBuilder>(path, settings);

            Assert.NotNull(result);
            Assert.Equal(value, result.ToString());

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

        [Fact]
        public async Task XmlTestAsync()
        {
            string url = "https://jovemnerd.com.br/feed-nerdcast";
            
            using (var client = new HttpClient())
            {
                using (var stream = await client.GetStreamAsync(url))
                {
                    foreach (var item in Xml.GetElements(stream, "item"))
                    {
                        //foreach (var node in item.Nodes())
                        //{
                        //    if ((node.NodeType == XmlNodeType.Element) && (node is XElement element) && (element.Name == "guid"))
                        //    {
                        //        var serializer = new XmlSerializer(typeof(Item));
                        //        var i = (Item)serializer.Deserialize(item.CreateReader());
                        //    }
                        //}

                        var serializer = new XmlSerializer(typeof(Item));
                        using (var itemReader = item.CreateReader())
                        {
                            var i = (Item)serializer.Deserialize(itemReader);
                            Assert.NotNull(i.Title);
                            break;
                        }
                    }
                }
            }
        }

        [Theory]
        [InlineData(1024, "1 KiB")]
        [InlineData(1048576, "1 MiB")]
        [InlineData(33554432, "32 MiB")]
        [InlineData(5864228747, "5.461488615 GiB")]
        [InlineData(0, "0 B")]
        public void ByteConversionToStringTest(ulong bytes, string expected)
        {
            // Set the thread culture so the current system culture don't interfere with the results
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");
            var converted = new ByteConversion(bytes);

            Assert.Equal(expected, converted.ToString());
        }

        [Theory]
        [InlineData(1024, Prefix.KiB, 1)]
        [InlineData(1048576, Prefix.MB, 1.048576)]
        [InlineData(33554432, Prefix.GiB, 0.03125)]
        [InlineData(5864228747, Prefix.GB, 5.864228747)]
        [InlineData(1000, Prefix.KB, 1)]
        [InlineData(0, Prefix.MB, 0)]
        public void ByteConversionPrefixTest(ulong bytes, Prefix prefix, double expected)
        {
            var converted = new ByteConversion(bytes, prefix);

            Assert.Equal(expected, converted.ConvertedBytes); ;
        }
        [Theory]
        [InlineData(1024, PrefixKind.Decimal, 1.024)]
        [InlineData(1048576, PrefixKind.Binary, 1)]
        [InlineData(1500000, PrefixKind.Decimal, 1.5)]
        [InlineData(33554432, PrefixKind.Decimal, 33.554432)]
        [InlineData(5864228747, PrefixKind.Binary, 5.4614886147901416)]
        [InlineData(1000, PrefixKind.Decimal, 1)]
        [InlineData(0, PrefixKind.Decimal, 0)]
        public void ByteConversionKindTest(ulong bytes, PrefixKind kind, double expected)
        {
            var converted = new ByteConversion(bytes, kind);

            Assert.Equal(expected, converted.ConvertedBytes);
        }
    }

    [XmlRoot("item")]
    public class Item
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
    }
}
