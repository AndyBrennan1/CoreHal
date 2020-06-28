using System;
using Xunit;
using CoreHal.Graph;

namespace CoreHal.Tests
{
    [Collection("Curie Tests")]
    public class CurieTests
    {
        [Fact]
        public void Constructing_WithNullKeyProvided_ThrowsException()
        {
            string key = null;
            string url = "/api/orders/123";

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithEmptyKeyProvided_ThrowsException()
        {
            string key = string.Empty;
            string url = "/api/orders/123";

            Assert.Throws<ArgumentException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithNullHrefProvided_ThrowsException()
        {
            string key = "XX";
            string url = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithEmptyHrefProvided_ThrowsException()
        {
            string key = "XX";
            string url = string.Empty;

            Assert.Throws<ArgumentException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithNonsenseHrefProvided_ThrowsException()
        {
            string key = "XX";
            string url = "I'm just some text";

            Assert.Throws<ArgumentException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithValidKeyAndRelativeUrl_CreatesCurie()
        {
            var key = "XX";
            var url = "/api/orders/{order-id}";

            var expectedUri = new Uri(url, UriKind.Relative);

            var curie = new Curie(key, url);

            Assert.Equal(expected: key, actual: curie.Key);
            Assert.Equal(expected: expectedUri, actual: curie.Href);
        }

        [Fact]
        public void Constructing_WithValidKeyAndAbsoluteUrl_CreatesCurie()
        {
            var key = "XX";
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var expectedUri = new Uri(url, UriKind.Absolute);

            var curie = new Curie(key, url);

            Assert.Equal(expected: key, actual: curie.Key);
            Assert.Equal(expected: expectedUri, actual: curie.Href);
        }

        [Fact]
        public void Constructing_WithUrlContainingNoTemplatePlaceholder_ThrowsException()
        {
            var key = "XX";
            var url = "http://www.myapi.com/api/orders/123";

            Assert.Throws<ArgumentException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithUrlContainingMoreThan1TemplatePlaceholder_ThrowsException()
        {
            var key = "XX";
            var url = "http://www.myapi.com/api/{category}/{category-id}";

            Assert.Throws<ArgumentException>(() =>
            {
                new Curie(key, url);
            });
        }

        [Fact]
        public void Constructing_WithTemplatePlaceholderInMiddleOfUrl_ThrowsException()
        {
            var key = "XX";
            var url = "http://www.myapi.com/api/{category}/123";

            Assert.Throws<ArgumentException>(() =>
            {
                new Curie(key, url);
            });
        }
    }
}
