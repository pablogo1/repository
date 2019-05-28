using System;

namespace Repository.Core
{
    public class PagingOptions
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public void Validate()
        {
            if(PageIndex < 1) throw new ArgumentOutOfRangeException(nameof(PageIndex));
            if(PageSize < 1) throw new ArgumentOutOfRangeException(nameof(PageSize));
        }

        public static PagingOptions Create(int pageIndex, int pageSize) => new PagingOptions
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}
