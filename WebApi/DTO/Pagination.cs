using System.Collections.Generic;

namespace WebApi.DTO
{
    public class Pagination<T> where T : class
    {
        public int Count { get; set; }

        public int PageIndex { get; set; } 

        public IReadOnlyList<T> Data { get; set; }

        public int pageCount { get; set; }
        public int PageSize { get; internal set; }
    }
}
