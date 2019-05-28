using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Repository.Core.Tests
{
    public class PagingExtensionsTests
    {
        private readonly IList<int> testData;

        public PagingExtensionsTests()
        {
            testData = new List<int> 
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };
        }

        [Fact]
        public void Page_ShouldReturnAllItems_WhenPageOptionsAreNull()
        {
            var testQueryable = testData.AsQueryable();

            var actual = testQueryable.Page(null);

            Assert.NotNull(actual);
            Assert.Equal(testData.Count, actual.ToList().Count);
        }

        [Theory]
        [InlineData(1, 5, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(2, 5, new int[] { 6, 7, 8, 9 })]
        [InlineData(3, 5, new int[] { })]
        public void Page_ShouldReturnCorrectSetOfPagedElements(int pageIndex, int pageSize, int[] expectedItems)
        {
            var testQueryable = testData.AsQueryable();

            var actualItems = testQueryable.Page(pageIndex, pageSize);

            Assert.Equal(expectedItems, actualItems.ToArray());
        }
    }
}
