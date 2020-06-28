using System;
using Xunit;
using CoreHal.Graph;

namespace CoreHal.Tests
{
    [Collection("Link Tests")]
    public class LinkTests
    {
        [Fact]
        public void Constructing_WithNullHrefProvided_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Link(null);
            });
        }

        [Fact]
        public void Constructing_WithEmptyHrefProvided_ThrowsException()
        {
            var emptyHref = string.Empty;

            Assert.Throws<ArgumentException>(() =>
            {
                new Link(emptyHref);
            });
        }

        [Fact]
        public void Constructing_WithNonsenseHrefProvided_ThrowsException()
        {
            var nonsenseString = "I'm just some text";

            Assert.Throws<ArgumentException>(() =>
            {
                new Link(nonsenseString);
            });
        }

        [Fact]
        public void Constructing_WithValidRelativeUrl_SetsHref()
        {
            var url = "/api/orders/123";

            var expectedUri = new Uri(url, UriKind.Relative);

            var link = new Link(url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithValidAbsoluteUrl_SetsHref()
        {
            var url = "http://www.myapi.com/api/orders/123";

            var expectedUri = new Uri(url, UriKind.Absolute);

            var link = new Link(url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithUrlContainingTemplatePlaceholder_SetsHref()
        {
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var expectedUri = new Uri(url, UriKind.Absolute);

            var link = new Link(url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithJustHref_SetsTitleToNull()
        {
            var url = "http://www.myapi.com/api/orders/123";

            var link = new Link(url);

            Assert.Null(link.Title);
        }

        [Fact]
        public void Constructing_WithJustHref_SetsTemplatedToFalse()
        {
            var url = "http://www.myapi.com/api/orders/123";

            var link = new Link(url);

            Assert.False(link.Templated);
        }

        [Fact]
        public void Constructing_WithJustHrefWithTemplatePlaceHolder_SetsTemplatedToTrue()
        {
            var url = "http://www.myapi.com/api/orders/{order-id}";

            var link = new Link(url);

            Assert.True(link.Templated);
        }

        [Fact]
        public void Constructing_WithJustHrefThatHasTrailingSlash_RemovesTrailingSlash()
        {
            var url = "http://www.myapi.com/api/orders/123/";

            var expectedUri = new Uri("http://www.myapi.com/api/orders/123", UriKind.Absolute);

            var link = new Link(url);

            Assert.Equal(expected: expectedUri, actual: link.Href);
        }

        [Fact]
        public void Constructing_WithNullHrefAndTitleProvided_ThrowsException()
        {
            string href = null;
            string title = "My human readable title";

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Link(href, title);
            });
        }

        [Fact]
        public void Constructing_WithEmptyHrefAndTitleProvided_ThrowsException()
        {
            string href = string.Empty;
            string title = "My human readable title";

            Assert.Throws<ArgumentException>(() =>
            {
                new Link(href, title);
            });
        }

        [Fact]
        public void Constructing_WithHrefAndTitle_SetsBoth()
        {
            var url = "http://www.myapi.com/api/orders/123/";
            var title = "My Title";

            var expectedUri = new Uri("http://www.myapi.com/api/orders/123", UriKind.Absolute);

            var link = new Link(url, title);

            Assert.Equal(expected: expectedUri, actual: link.Href);
            Assert.Equal(expected: title, actual: link.Title);
        }

        [Fact]
        public void Constructing_WithUrlContainingMoreThan1TemplatePlaceholder_ThrowsException()
        {
            var url = "http://www.myapi.com/api/{category}/{category-id}";

            Assert.Throws<ArgumentException>(() =>
            {
                new Link(url);
            });
        }

        [Fact]
        public void Constructing_WithTemplatePlaceholderInMiddleOfUrl_ThrowsException()
        {
            var url = "http://www.myapi.com/api/{category}/123";

            Assert.Throws<ArgumentException>(() =>
            {
                new Link(url);
            });
        }
    }
}