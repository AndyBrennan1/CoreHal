using CoreHal.PropertyNaming;
using System;
using Xunit;

namespace CoreHal.Tests
{
    [Collection("Curie Link Tests")]
    public class CamelDashNameConventionTests
    {
        [Fact]
        public void ApplyingNaming_WithNullPropertyNameProvided_ThrowsException()
        {
            string propertyName = null;

            var namingConvention = new CamelDashNameConvention();

            Assert.Throws<ArgumentNullException>(() =>
            {
                namingConvention.Apply(propertyName);
            });
        }

        [Fact]
        public void ApplyingNaming_WithEmptyPropertyNameProvided_ThrowsException()
        {
            string propertyName = string.Empty;

            var namingConvention = new CamelDashNameConvention();

            Assert.Throws<ArgumentException>(() =>
            {
                namingConvention.Apply(propertyName);
            });
        }

        [Fact]
        public void Apply_WithOneWordPropertyNameInAllLowerCase_ReturnsAllLowerCasePropertyName()
        {
            string propertyName = "lower";
            string expectedResult = "lower";

            var namingConvention = new CamelDashNameConvention();

            var result = namingConvention.Apply(propertyName);

            Assert.Equal(expected: expectedResult, actual: result);
        }

        [Fact]
        public void Apply_WithOneWordPropertyNameWithFirstLetterCapitalised_ReturnsAllLowerCasePropertyName()
        {
            string propertyName = "Lower";
            string expectedResult = "lower";

            var namingConvention = new CamelDashNameConvention();

            var result = namingConvention.Apply(propertyName);

            Assert.Equal(expected: expectedResult, actual: result);
        }

        [Fact]
        public void Apply_WithOneWordPropertyNameWithAllLettersCapitalised_ReturnsAllLowerCasePropertyName()
        {
            string propertyName = "SKU";
            string expectedResult = "sku";

            var namingConvention = new CamelDashNameConvention();

            var result = namingConvention.Apply(propertyName);

            Assert.Equal(expected: expectedResult, actual: result);
        }

        [Fact]
        public void Apply_WithTwoWordPropertyNameWithTitleCase_ReturnsCamlecaseAndHyphenSeparated()
        {
            string propertyName = "TwoWords";
            string expectedResult = "two-Words";

            var namingConvention = new CamelDashNameConvention();

            var result = namingConvention.Apply(propertyName);

            Assert.Equal(expected: expectedResult, actual: result);
        }

        [Fact]
        public void Apply_WithThreeWordPropertyNameWithTitleCase_ReturnsCamlecaseAndHyphenSeparated()
        {
            string propertyName = "ThreeWordsNow";
            string expectedResult = "three-Words-Now";

            var namingConvention = new CamelDashNameConvention();

            var result = namingConvention.Apply(propertyName);

            Assert.Equal(expected: expectedResult, actual: result);
        }
    }
}