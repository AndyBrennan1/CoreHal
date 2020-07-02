using CoreHal.Graph;
using CoreHal.Tests.Fixtures;
using CoreHal.Tests.Fixtures.PropertyTypes;
using DeepEqual.Syntax;
using System;
using System.Collections.Generic;
using Xunit;

namespace CoreHal.Tests
{
    [Collection("HAL Graph Tests")]
    public class HalGraphTests
    {
        [Fact]
        public void Constructing_WithNullModelProvided_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new HalGraph(null);
            });
        }

        [Fact]
        public void Constructing_WithCollectionModelProvided_ThrowsException()
        {
            var modelList = new List<ExampleModel>();

            Assert.Throws<ArgumentException>(() =>
            {
                new HalGraph(modelList);
            });
        }

        [Fact]
        public void Constructing_WithModelProvided_SetsModelType()
        {
            var model = new SimpleModel();

            var graph = new HalGraph(model);

            Assert.True(graph.ModelType == typeof(SimpleModel));
        }

        [Fact]
        public void Constructing_WithSinglePropertyModelProvided_AddsProperty()
        {
            const string expectedKeyName = "StringProperty";
            const string expectedValue = "I AM A STRING FIELD";

            var model = new SimpleModel { StringProperty = expectedValue };

            var graph = new HalGraph(model);

            Assert.True(graph.Contains(expectedKeyName));
            Assert.Equal(expected: expectedValue, actual: graph[expectedKeyName]);
        }

        [Fact]
        public void Constructing_WithModelHavingComplexPropertyProvided_AddsPropertyAsDictionary()
        {
            const string expectedKeyName = "ComplexProperty";
            const string expectedValue = "I AM A STRING FIELD";

            var complexProperty = new SimpleModel { StringProperty = expectedValue };

            var expectedComplexPropertyDictionary = new Dictionary<string, object>
            {
                { "StringProperty", expectedValue }
            };

            var model = new ModelWith1ComplexProperty { ComplexProperty = complexProperty };

            var graph = new HalGraph(model);

            Assert.True(graph.Contains(expectedKeyName));
            Assert.Equal(expected: expectedComplexPropertyDictionary, actual: graph[expectedKeyName]);
        }

        [Fact]
        public void Constructing_WithModelContainingNoOutputProperty_DoesNotAddProperty()
        {
            const string fieldExpectedToBeMissing = "Integer2";

            var model = new ExampleWithIgnoredProperty
            {
                Integer1 = 1,
                Integer2 = 2,
                Integer3 = 3
            };

            var graph = new HalGraph(model);

            Assert.True(!graph.Contains(fieldExpectedToBeMissing));
        }

        [Fact]
        public void Constructing_WithModelContainingIntegerProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const int value = 999;

            var model = new ModelWithIntegerProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingInt32Property_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const Int32 value = 999;

            var model = new ModelWithInt32Property { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingDecimalProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const decimal value = 123.0M;

            var model = new ModelWithDecimalProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingFloatProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const float value = 134.45E-2f;

            var model = new ModelWithFloatProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingDoubleProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const double value = 0.42e2;

            var model = new ModelWithDoubleProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingDateTimeProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            DateTime value = new DateTime(2020, 6, 28);

            var model = new ModelWithDateTimeProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingBooleanProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const bool value = true;

            var model = new ModelWithBooleanProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingStringProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            const string value = "MY-VALUE";

            var model = new ModelWithStringProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingGuidProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            Guid value = Guid.Parse("346d6b20-a2ec-4bf1-98c4-bbf897e5f87a");

            var model = new ModelWithGuidProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingTimeSpanProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            TimeSpan value = new TimeSpan(1000);

            var model = new ModelWithTimeSpanProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingNullableIntegerProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            int? value = 12;

            var model = new ModelWithNullableIntegerProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingUserDefinedReferenceTypeProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            ModelWithBooleanProperty value = new ModelWithBooleanProperty { Property = true };

            var model = new ModelWithUserDefinedReferenceTypeProperty { Property = value };

            var graph = new HalGraph(model);

            var expectedResult = new Dictionary<string, object>
            {
                { "Property",  true }
            };

            Assert.Equal(expected: expectedResult, graph[propertyName]);
            Assert.Equal(expected: expectedResult.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingUserDefinedStructProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            var model = new ModelWithUserDefinedStructProperty() { Property = new UserDefinedStruct("value") };

            var graph = new HalGraph(model);

            var expectedResult = new Dictionary<string, object>
            {
                { "Property",  "value" }
            };

            Assert.Equal(expected: expectedResult, graph[propertyName]);
            Assert.Equal(expected: expectedResult.GetType(), actual: graph[propertyName].GetType());
        }

        [Fact]
        public void Constructing_WithModelContainingUriProperty_AddsThePropertyAsExpected()
        {
            const string propertyName = "Property";
            Uri value = new Uri("http://www.google.com");

            var model = new ModelWithUriProperty { Property = value };

            var graph = new HalGraph(model);

            Assert.Equal(expected: value, graph[propertyName]);
            Assert.Equal(expected: value.GetType(), actual: graph[propertyName].GetType());
        }


        [Fact]
        public void Constructing_WithPropertyValueSetToNull_AddsThePropertyWithNullValue()
        {
            var model = new SimpleModel() { StringProperty = null };

            var graph = new HalGraph(model);

            Assert.Null(graph["StringProperty"]);
        }

        [Fact]
        public void AddingLink_WithNullRel_ThrowsException()
        {
            string rel = null;

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var linkToAdd = new Link("/api/some-resource");

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddLink(rel, linkToAdd);
            });
        }

        [Fact]
        public void AddingLink_WithEmptyRel_ThrowsException()
        {
            string rel = "";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var linkToAdd = new Link("/api/some-resource");

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddLink(rel, linkToAdd);
            });
        }

        [Fact]
        public void AddingLink_WhichIsNull_ThrowsException()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddLink(rel, null);
            });
        }

        [Fact]
        public void AddingLink_WhenValid_ReturnsTheHalGraph()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var expectedLink = new Link("/api/some-resource");

            var result = graph.AddLink(rel, expectedLink);

            Assert.Equal(expected: graph, actual: result);
        }

        [Fact]
        public void AddingLink_WhenValid_MakesLinksTheFirstKey()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var expectedLink = new Link("/api/some-resource");

            var result = (HalGraph)graph.AddLink(rel, expectedLink);

            Assert.IsType<Dictionary<string, object>>(result[0]);
        }

        [Fact]
        public void AddingLink_WhenValid_GetsAddedToTheLinkCollection()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var expectedLink = new Link("/api/some-resource");

            graph.AddLink(rel, expectedLink);

            Assert.True(((Dictionary<string, object>)graph["_links"]).Count == 1);
            Assert.Equal(
                expected: expectedLink,
                actual: ((Dictionary<string, object>)graph["_links"])[rel]);
        }

        [Fact]
        public void AddingLink_WhenRelAlreadyExists_AddsLinkToExistingRelCollection()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var firstLink = new Link("/api/some-resource1");
            var secondLink = new Link("/api/some-resource2");

            graph.AddLink(rel, firstLink);
            graph.AddLink(rel, secondLink);

            var linksCollection = (List<Link>)((Dictionary<string, object>)graph["_links"])[rel];

            Assert.True(((Dictionary<string, object>)graph["_links"]).Count == 1);
            Assert.True(linksCollection.Count == 2);
            Assert.Equal(expected: firstLink, actual: linksCollection[0]);
            Assert.Equal(expected: secondLink, actual: linksCollection[1]);
        }

        [Fact]
        public void AddingMultipleLinks_WithNullRel_ThrowsException()
        {
            string rel = null;

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var linksToAdd = new List<Link> {
                new Link("/api/some-resource1"),
                new Link("/api/some-resource2")
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddLinks(rel, linksToAdd);
            });
        }

        [Fact]
        public void AddingMultipleLinks_WithEmptyRel_ThrowsException()
        {
            string rel = string.Empty;

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var linksToAdd = new List<Link> {
                new Link("/api/some-resource1"),
                new Link("/api/some-resource2")
            };

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddLinks(rel, linksToAdd);
            });
        }

        [Fact]
        public void AddingMultipleLinks_WithNullLinksCollection_ThrowsException()
        {
            string rel = "my-rel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            List<Link> linksToAdd = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddLinks(rel, linksToAdd);
            });
        }

        [Fact]
        public void AddingMultipleLinks_WithEmptyLinksCollection_ThrowsException()
        {
            string rel = "my-rel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var linksToAdd = new List<Link>();

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddLinks(rel, linksToAdd);
            });
        }

        [Fact]
        public void AddingMultipleLink_WhenValid_ReturnsTheHalGraph()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var firstLink = new Link("/api/some-resource1");
            var secondLink = new Link("/api/some-resource2");
            var thirdLink = new Link("/api/some-resource3");
            var fourthLink = new Link("/api/some-resource4");

            var result = graph.AddLinks(rel, new List<Link> { firstLink, secondLink, thirdLink, fourthLink });

            Assert.Equal(expected: graph, actual: result);
        }

        [Fact]
        public void AddingMultipleLink_WhenValid_AddsThemAllToTheSameRelCollection()
        {
            const string rel = "myrel";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var firstLink = new Link("/api/some-resource1");
            var secondLink = new Link("/api/some-resource2");
            var thirdLink = new Link("/api/some-resource3");
            var fourthLink = new Link("/api/some-resource4");

            graph.AddLinks(rel, new List<Link> { firstLink, secondLink, thirdLink, fourthLink });

            var linksCollection = (List<Link>)((Dictionary<string, object>)graph["_links"])[rel];

            Assert.True(((Dictionary<string, object>)graph["_links"]).Count == 1);
            Assert.True(linksCollection.Count == 4);
            Assert.Equal(expected: firstLink, actual: linksCollection[0]);
            Assert.Equal(expected: secondLink, actual: linksCollection[1]);
            Assert.Equal(expected: thirdLink, actual: linksCollection[2]);
            Assert.Equal(expected: fourthLink, actual: linksCollection[3]);
        }

        [Fact]
        public void AddingCurie_WhichIsNull_ThrowsException()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddCurie(null);
            });
        }

        [Fact]
        public void AddingCurie_WhenValid_ReturnsTheHalGraph()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var expectedCurie = new Curie("xx", "http://www.myapi.com/api/{placeholder}");

            var result = graph.AddCurie(expectedCurie);

            Assert.Equal(expected: graph, actual: result);
        }

        [Fact]
        public void AddingCurie_WhenValid_MakesLinksTheFirstKey()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var curieToCreate = new Curie("xx", "http://www.myapi.com/api/{placeholder}");

            var result = (HalGraph)graph.AddCurie(curieToCreate);

            Assert.IsType<Dictionary<string, object>>(result[0]);
        }

        [Fact]
        public void AddingCurie_WhenValid_CreatesCurieCollection()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var curieToCreate = new Curie("xx", "http://www.myapi.com/api/{something}");

            graph.AddCurie(curieToCreate);

            var actualLink = ((Dictionary<string, object>)graph["_links"])["curies"];

            Assert.IsAssignableFrom<IEnumerable<Link>>(actualLink);
        }

        [Fact]
        public void AddingCurie_WhenValid_CreateLinkWithNameCurie()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var curieToCreate = new Curie("xx", "http://www.myapi.com/api/{something}");

            var expectedLink = new CurieLink("xx", "http://www.myapi.com/api/{something}");

            graph.AddCurie(curieToCreate);

            var curiesCollection = (List<CurieLink>)((Dictionary<string, object>)graph["_links"])["curies"];

            Assert.True(((Dictionary<string, object>)graph["_links"]).Count == 1);
            Assert.True(curiesCollection.Count == 1);
            expectedLink.ShouldDeepEqual(curiesCollection[0]);
        }

        [Fact]
        public void AddingCurie_WhenOneAlreadyAdded_AddsTheCurieToTheExistingCurieCollection()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var firstCurieToCreate = new Curie("aa", "http://www.myapi.com/api/orders/{order-id}");
            var secondCurieToCreate = new Curie("bb", "http://www.myapi.com/api/customers/{customer-id}");

            //var expectedLink = new CurieLink("http://www.myapi.com/api/{something}");

            graph
                .AddCurie(firstCurieToCreate)
                .AddCurie(secondCurieToCreate);

            var curiesCollection = (List<CurieLink>)((Dictionary<string, object>)graph["_links"])["curies"];

            Assert.True(((Dictionary<string, object>)graph["_links"]).Count == 1);
            Assert.True(curiesCollection.Count == 2);
        }

        [Fact]
        public void AddingMultipleCuries_WithNullCuriesCollection_ThrowsException()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            List<Curie> curiesToAdd = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddCuries(curiesToAdd);
            });
        }

        [Fact]
        public void AddingMultipleCuries_WithEmptyCuriesCollection_ThrowsException()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var curiesToAdd = new List<Curie>();

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddCuries(curiesToAdd);
            });
        }

        [Fact]
        public void AddingMultipleCuries_WhenValid_ReturnsTheHalGraph()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var firstCurie = new Curie("a", "/api/some-resource1/{template}");
            var secondCurie = new Curie("b", "/api/some-resource2/{template}");
            var thirdCurie = new Curie("c", "/api/some-resource3/{template}");
            var fourthCurie = new Curie("d", "/api/some-resource4/{template}");

            var result = graph.AddCuries(new List<Curie> { firstCurie, secondCurie, thirdCurie, fourthCurie });

            Assert.Equal(expected: graph, actual: result);
        }

        [Fact]
        public void AddingMultipleCuries_WhenValid_AddsThemAllToTheCollection()
        {
            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var firstCurie = new Curie("a", "/api/some-resource1/{template}");
            var secondCurie = new Curie("b", "/api/some-resource2/{template}");
            var thirdCurie = new Curie("c", "/api/some-resource3/{template}");
            var fourthCurie = new Curie("d", "/api/some-resource4/{template}");

            graph.AddCuries(new List<Curie> { firstCurie, secondCurie, thirdCurie, fourthCurie });

            var curiesCollection = (List<CurieLink>)((Dictionary<string, object>)graph["_links"])["curies"];

            var expectedCurieLink1 = new CurieLink("a", "/api/some-resource1/{template}");
            var expectedCurieLink2 = new CurieLink("b", "/api/some-resource2/{template}");
            var expectedCurieLink3 = new CurieLink("c", "/api/some-resource3/{template}");
            var expectedCurieLink4 = new CurieLink("d", "/api/some-resource4/{template}");

            Assert.True(((Dictionary<string, object>)graph["_links"]).Count == 1);
            Assert.True(curiesCollection.Count == 4);
            expectedCurieLink1.ShouldDeepEqual(curiesCollection[0]);
            expectedCurieLink2.ShouldDeepEqual(curiesCollection[1]);
            expectedCurieLink3.ShouldDeepEqual(curiesCollection[2]);
            expectedCurieLink4.ShouldDeepEqual(curiesCollection[3]);
        }

        [Fact]
        public void AddingEmbeddedItem_WhichIsNull_ThrowsException()
        {
            const string embeddedItemName = "some-name";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddEmbeddedItem(embeddedItemName, null);
            });
        }

        [Fact]
        public void AddingEmbeddedItem_WithEmptyName_ThrowsException()
        {
            var embeddedItemName = string.Empty;

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var embeddedGraph = new HalGraph(model);

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddEmbeddedItem(embeddedItemName, embeddedGraph);
            });
        }

        [Fact]
        public void AddingEmbeddedItem_WhenValid_ReturnsTheHalGraph()
        {
            const string embeddedItemName = "SOME-VALID-STRING";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var expectedEmbeddedGraph = new HalGraph(model);

            var result = graph.AddEmbeddedItem(embeddedItemName, expectedEmbeddedGraph);

            Assert.Equal(expected: graph, actual: result);
        }

        [Fact]
        public void AddingEmbeddedItem_WhenValid_MakesEmbeddedItemsTheLastKey()
        {
            const string embeddedItemName = "SOME-VALID-STRING";

            var modelAtTopLevel = new SimpleModel();
            var graph = new HalGraph(modelAtTopLevel);

            var modelForEmbedded = new SimpleModel();
            var expectedEmbeddedGraph = new HalGraph(modelForEmbedded);

            var result = (HalGraph)graph.AddEmbeddedItem(embeddedItemName, expectedEmbeddedGraph);

            var lastKeyIndex = result.Count;

            Assert.IsType<Dictionary<string, object>>(result[lastKeyIndex-1]);
        }

        [Fact]
        public void AddingEmbeddedItem_WhenValid_AddsItToTheCollection()
        {
            const string embeddedItemName = "SOME-VALID-STRING";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var expectedEmbeddedGraph = new HalGraph(model);

            graph.AddEmbeddedItem(embeddedItemName, expectedEmbeddedGraph);

            var storedHalGraph = (IHalGraph)((Dictionary<string, object>)graph["_embedded"])[embeddedItemName];

            Assert.True(((Dictionary<string, object>)graph["_embedded"]).Count == 1);
            Assert.Equal(expected: expectedEmbeddedGraph, actual: storedHalGraph);
        }

        [Fact]
        public void AddingEmbeddedItem_WithDuplicateKeyName_ThrowsException()
        {
            const string initialKey = "I-AM-THE-KEY-THE-FIRST-TIME";
            const string secondKey = "I-AM-THE-KEY-THE-FIRST-TIME";

            var model = new SimpleModel();
            var parentGraph = new HalGraph(model);

            var childModel1 = new SimpleModel();
            var childModel2 = new SimpleModel();

            var childGraph1 = new HalGraph(childModel1);
            var childGraph2 = new HalGraph(childModel2);

            parentGraph.AddEmbeddedItem(initialKey, childGraph1);

            Assert.Throws<ArgumentException>(() =>
            {
                parentGraph.AddEmbeddedItem(secondKey, childGraph2);
            });
        }

        [Fact]
        public void AddingEmbeddedItemCollection_WhichIsNull_ThrowsException()
        {
            const string embeddedItemName = "some-name";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            Assert.Throws<ArgumentNullException>(() =>
            {
                graph.AddEmbeddedItemCollection(embeddedItemName, null);
            });
        }

        [Fact]
        public void AddingEmbeddedItemCollection_WithEmptyName_ThrowsException()
        {
            var embeddedItemName = string.Empty;

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var embeddedGraph1 = new HalGraph(model);
            var embeddedGraph2 = new HalGraph(model);

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddEmbeddedItemCollection(
                    embeddedItemName,
                    new List<IHalGraph> { embeddedGraph1, embeddedGraph2 });
            });
        }

        [Fact]
        public void AddingEmbeddedItemCollection_WhenValid_ReturnsTheHalGraph()
        {
            const string embeddedItemName = "SOME-VALID-STRING";

            var model = new SimpleModel();
            var graph = new HalGraph(model);

            var embeddedGraph1 = new HalGraph(model);
            var embeddedGraph2 = new HalGraph(model);

            var result = graph.AddEmbeddedItemCollection(
                embeddedItemName,
                new List<IHalGraph> { embeddedGraph1, embeddedGraph2 });

            Assert.Equal(expected: graph, actual: result);
        }

        [Fact]
        public void AddingEmbeddedItemCollection_WhenValid_AddsItToTheCollection()
        {
            const string embeddedItemName = "SOME-VALID-STRING";

            var modelParent = new SimpleModel { StringProperty = "Some string value" };
            var modelChild1 = new ExampleModel { Id = Guid.NewGuid(), Name = "SOME NAME" };
            var modelChild2 = new ExampleModel { Id = Guid.NewGuid(), Name = "ANOTHER NAME" };
            var graph = new HalGraph(modelParent);

            var embeddedGraph1 = new HalGraph(modelChild1);
            var embeddedGraph2 = new HalGraph(modelChild2);

            graph.AddEmbeddedItemCollection(
                embeddedItemName,
                new List<IHalGraph> { embeddedGraph1, embeddedGraph2 });

            var storedHalGraph = (List<IHalGraph>)((Dictionary<string, object>)graph["_embedded"])[embeddedItemName];

            Assert.True(((Dictionary<string, object>)graph["_embedded"]).Count == 1);
            Assert.True(storedHalGraph.Count == 2);
            Assert.Equal(expected: embeddedGraph1, actual: storedHalGraph[0]);
            Assert.Equal(expected: embeddedGraph2, actual: storedHalGraph[1]);
        }

        [Fact]
        public void AddingEmbeddedItemCollection_WithVaryingModelType_ThrowsException()
        {
            const string embeddedItemName = "SOME-VALID-STRING";

            var modelParent = new SimpleModel { StringProperty = "Some string value" };
            var modelChild1 = new ExampleModel { Id = Guid.NewGuid(), Name = "SOME NAME" };
            var modelChild2IsOfDifferentType = new SimpleModelWithDateField { SomeDateField = DateTime.Now };

            var graph = new HalGraph(modelParent);
            var embeddedGraph1 = new HalGraph(modelChild1);
            var embeddedGraph2 = new HalGraph(modelChild2IsOfDifferentType);

            Assert.Throws<ArgumentException>(() =>
            {
                graph.AddEmbeddedItemCollection(
                embeddedItemName,
                new List<IHalGraph> { embeddedGraph1, embeddedGraph2 });
            });
        }

        [Fact]
        public void AddingEmbeddedItemCollection_WithDuplicateKeyName_ThrowsException()
        {
            const string initialKey = "I-AM-THE-KEY-THE-FIRST-TIME";
            const string secondKey = "I-AM-THE-KEY-THE-FIRST-TIME";

            var model = new SimpleModel();
            var parentGraph = new HalGraph(model);

            var childModel1 = new SimpleModel();
            var childModel2 = new SimpleModel();

            var childGraph1 = new HalGraph(childModel1);
            var childGraph2 = new HalGraph(childModel2);

            parentGraph.AddEmbeddedItemCollection(
                initialKey,
                new List<IHalGraph> { childGraph1, childGraph2 });

            Assert.Throws<ArgumentException>(() =>
            {
                parentGraph.AddEmbeddedItemCollection(
                    secondKey,
                    new List<IHalGraph> { childGraph1, childGraph2 });
            });
        }
    }
}