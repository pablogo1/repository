using System;
using Xunit;

namespace Repository.Core.Tests
{
    public class PageOptionsTests
    {
        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenPageIndexIsZeroOrLess()
        {
            var doTest = new Action<int, int>((int pageIndex, int pageSize) => {
                var pageOptions = CreateOptions(pageIndex, pageSize);

                var exception = Assert.ThrowsAny<ArgumentOutOfRangeException>(() => pageOptions.Validate());
                Assert.Equal(nameof(PagingOptions.PageIndex), exception.ParamName);
            });

            doTest(0, 5);
            doTest(-1, 5);
        }

        [Fact]
        public void Validate_ShouldNotThrowException_WhenPageIndexIsGreaterThanZero()
        {
            var pageOptions = CreateOptions(1, 5);

            pageOptions.Validate();
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenPageSizeIsZeroOrLess()
        {
            var doTest = new Action<int, int>((int pageIndex, int pageSize) => {
                var pageOptions = CreateOptions(pageIndex, pageSize);

                var exception = Assert.ThrowsAny<ArgumentOutOfRangeException>(() => pageOptions.Validate());
                Assert.Equal(nameof(PagingOptions.PageSize), exception.ParamName);
            });

            doTest(5, 0);
            doTest(5, -1);
        }

        [Fact]
        public void Validate_ShouldNotThrowException_WhenPageSizeIsGreaterOrEqualToOne()
        {
            var doTest = new Action<int, int>((int pageIndex, int pageSize) => {
                var pageOptions = CreateOptions(pageIndex, pageSize);

                pageOptions.Validate();
            });

            doTest(5, 1);
            doTest(5, 2);
        }

        private PagingOptions CreateOptions(int pageIndex, int pageSize) => new PagingOptions 
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}
