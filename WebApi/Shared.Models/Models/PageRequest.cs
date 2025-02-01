using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Shared.Enums;

namespace WebApi.Shared.Models
{
    public class PageRequest
    {
        public List<Filter>? Filters { get; set; }
        public List<Sort>? Sorts { get; set; }
        public Pagination? Pagination { get; set; }
    }

    public class Filter
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }

    public class Sort
    {
        public string PropertyName { get; set; }
        public bool IsAscending { get; set; }
    }

    public class Pagination
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Skip => (PageNo - 1) * PageSize;
        public int Take => PageSize;

    }
}
