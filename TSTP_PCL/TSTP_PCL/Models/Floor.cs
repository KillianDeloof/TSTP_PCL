using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TSTP_PCL.Models
{
    public partial class Floor : BaseItem
    {
        public int FloorID { get; set; }
        public int BuildingID { get; set; }
        public int WingID { get; set; }
        public string UCODE { get; set; }
        public string UDESC { get; set; }
        public string CCODE { get; set; }
        public string CDESC { get; set; }

        [Ignore]
        public virtual Wing Wing { get; set; }
        public String IMKey { get; set; }

        public override string ToString()
        {
            return UDESC;
        }
    }
}
