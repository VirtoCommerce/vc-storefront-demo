using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Storefront.Model.Common;
using Xunit;

namespace VirtoCommerce.Storefront.Tests
{
    [Trait("Category", "Unit")]
    public class ValueObjectTests
    {
        public class ComplexObject : ValueObject
        {
            public string SimpleProperty { get; set; }
            public string AnotherSimpleProperty { get; set; }
            public List<string> ListProperty { get; set; }
            public List<string> AnotherListProperty { get; set; }
        }

        [Fact]
        public void ObjectsWithTheSameValuesShouldBeEqual()
        {
            var value1 = new ComplexObject
            {
                SimpleProperty = "A",
                ListProperty = new List<string> { "A", "B", "C" }
            };
            var value2 = new ComplexObject
            {
                SimpleProperty = "A",
                ListProperty = new List<string> { "A", "B", "C" }
            };
            var code1 = value1.GetHashCode();
            var code2 = value2.GetHashCode();
            Assert.Equal(code1, code2);
            Assert.Equal(value1, value2);
        }

        [Fact]
        public void ObjectsWithDifferentValuesShouldNotBeEqual()
        {
            var objectsWithDifferentValues = GetObjectsWithDifferentValues();

            // Compare each object with all other objects
            for (var i = 0; i < objectsWithDifferentValues.Count; i++)
            {
                for (var j = 0; j < objectsWithDifferentValues.Count; j++)
                {
                    if (i != j)
                    {
                        var object1 = objectsWithDifferentValues[i];
                        var object2 = objectsWithDifferentValues[j];

                        var equals = object1.Equals(object2);

                        Assert.False(equals, $"Objects #{i} and #{j} must not be equal.");
                    }
                }
            }
        }

        [Fact]
        public void ObjectsWithDifferentValuesShouldHaveDifferentCacheKeys()
        {
            var objectsWithDifferentValues = GetObjectsWithDifferentValues();

            var cacheKeys = objectsWithDifferentValues.Select(x => x.GetCacheKey()).ToList();
            var uniqueCacheKeys = cacheKeys.Distinct().ToList();

            Assert.Equal(objectsWithDifferentValues.Count, uniqueCacheKeys.Count);
        }


        private static IList<ComplexObject> GetObjectsWithDifferentValues()
        {
            return new[]
            {
                // All properties are null
                new ComplexObject(),

                new ComplexObject { SimpleProperty = "" },
                new ComplexObject { SimpleProperty = "null" },

                new ComplexObject { AnotherSimpleProperty = "" },
                new ComplexObject { AnotherSimpleProperty = "null" },

                // ListProperty is not null
                new ComplexObject { ListProperty = new List<string>() },
                new ComplexObject { ListProperty = new List<string> { null } },
                new ComplexObject { ListProperty = new List<string> { "" } },
                new ComplexObject { ListProperty = new List<string> { "null" } },

                // AnotherListProperty is not null
                new ComplexObject { AnotherListProperty = new List<string>() },
                new ComplexObject { AnotherListProperty = new List<string> { null } },
                new ComplexObject { AnotherListProperty = new List<string> { "" } },
                new ComplexObject { AnotherListProperty = new List<string> { "null" } },
            };
        }
    }
}
