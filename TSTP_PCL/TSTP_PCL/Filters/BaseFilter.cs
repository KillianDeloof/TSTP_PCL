using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSTP_PCL.Filters
{
    public class BaseFilter
    {
        public int PageSize { get; set; } = 20000;
        public int EffectivePageSize
        {
            get
            {
                if (PageSize < 1) return 1;
                if (PageSize > 100) return 100;
                return PageSize;
            }
        }
    }
}
