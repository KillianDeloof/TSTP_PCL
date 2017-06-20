using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TSTP_PCL.Models
{
    public partial class Building : BaseItem
    {

        public int CampusID { get; set; }
        public string Address { get; set; }
        // bevat bv RSS.T
        public string UCODE { get; set; }
        // bevat bv rss.t
        public string UDESC { get; set; }
        // bevat bv T
        public string CCODE { get; set; }
        // bevat bv "howest tijdelijke klassen"
        public string CDESC { get; set; }

        [Ignore]
        public virtual Campus Campus { get; set; }
        // -- uit te zoeken -- //
        public String IMKey { get; set; }

        public override string ToString()
        {
            return UDESC;
        }
    }
}
