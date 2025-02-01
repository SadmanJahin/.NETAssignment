using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Shared.Models
{
    public class PageResponse<T>
    {
        public List<T>? ListResponseData { get; set; }
        public long ResultCount { get; set; }
        public bool? HasNext { get; set; }
    }
}
