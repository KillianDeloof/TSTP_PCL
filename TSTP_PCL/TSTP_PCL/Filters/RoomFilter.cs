using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSTP_PCL.Models;

namespace TSTP_PCL.Filters
{
    public class RoomFilter : BaseFilter
    {
        public Room LastRoom { get; set; }
        public int? FloorID { get; set; }
    }
}
