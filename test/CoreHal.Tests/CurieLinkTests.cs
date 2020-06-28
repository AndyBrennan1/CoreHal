using CoreHal.Graph;
using System;
using Xunit;

namespace CoreHal.Tests
{
    [Collection("Curie Link Tests")]
    public class CurieLinkTests
    {
        [Fact]
        public void Constructing_WithNullNameProvided_ThrowsException()
        {
            string name = null;
            string href = "/api/orders/{order-id}";

            Assert.Throws<ArgumentNullException>(() =>
            {
                new CurieLink(name, href);
            });
        }

        [Fact]
        public void Constructing_WithEmptyNameProvided_ThrowsException()
        {
            string name = string.Empty;
            string href = "/api/orders/{order-id}";

            Assert.Throws<ArgumentException>(() =>
            {
                new CurieLink(name, href);
            });
        }

        [Fact]
        public void Constructing_WithNullHrefProvided_ThrowsException()
        {
            string name = "some-name";
            string href = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                new CurieLink(name, href);
            });
        }

        [Fact]
        public void Constructing_WithEmptyHrefProvided_ThrowsException()
        {
            string name = "some-name";
            string href = string.Empty;

            Assert.Throws<ArgumentException>(() =>
            {
                new CurieLink(name, href);
            });
        }

        [Fact]
        public void Constructing_WithValidRelativeUrlAndName_SetsHref()
        {
            var name = "my-name";
            var url = "/api/orders/{order-id}";

            var expectedUri = new Uri(url, UriKind.Relative);

            var link = new CurieLink(name, url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithValidRelativeUrlAndName_SetsName()
        {
            var name = "my-name";
            var url = "/api/orders/{order-id}";

            var link = new CurieLink(name, url);

            Assert.Equal(expected: name, actual: link.Name);
        }

        [Fact]
        public void Constructing_WithValidAbsoluteUrl_SetsHref()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var expectedUri = new Uri(url, UriKind.Absolute);

            var link = new CurieLink(name, url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithValidAbsoluteUrl_SetsName()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var link = new CurieLink(name, url);

            Assert.Equal(expected: name, actual: link.Name);
        }

        [Fact]
        public void Constructing_ValidUrlButHasTrailingSlash_TrailingSlashIsRemoved()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/orders/{order-id}/";

            var expectedUrl = new Uri("http://www.myapi.com/api/orders/{order-id}", UriKind.Absolute);

            var link = new CurieLink(name, url);

            Assert.Equal(expected: expectedUrl, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithUrlContainingTemplatePlaceholder_SetsHref()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var expectedUri = new Uri(url, UriKind.Absolute);

            var link = new CurieLink(name, url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithUrlContainingTemplatePlaceholder_SetsName()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var link = new CurieLink(name, url);

            Assert.Equal(expected: name, actual: link.Name);
        }

        [Fact]
        public void Constructing_WithNoTemplatePaceHolderInUrl_ThrowsException()
        {
            string name = "some-name";
            string href = "/api/orders/123";

            Assert.Throws<ArgumentException>(() =>
            {
                new CurieLink(name, href);
            });
        }

        [Fact]
        public void Constructing_WithUrlContainingMoreThan1TemplatePlaceholder_ThrowsException()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/{category}/{category-id}";

            Assert.Throws<ArgumentException>(() =>
            {
                new CurieLink(name, url);
            });
        }

        [Fact]
        public void Constructing_WithTemplatePlaceholderInMiddleOfUrl_ThrowsException()
        {
            var name = "my-name";
            var url = "http://www.myapi.com/api/{category}/123";

            Assert.Throws<ArgumentException>(() =>
            {
                new CurieLink(name, url);
            });
        }
    }
}