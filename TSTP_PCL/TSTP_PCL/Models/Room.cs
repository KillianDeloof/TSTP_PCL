using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TSTP_PCL.Models
{
    public class Room : BaseItem
    {
        public int FloorID { get; set; }
        public string UCODE { get; set; }
        public string UDESC { get; set; }
        public string CCODE { get; set; }
        public string CDESC { get; set; }      
        public string Number { get; set; }
        public String RoomInfo { get; set; }

        [Ignore]
        public int? RoomCategoryID { get; set; }

        [Ignore]
        public int? RoomFunctionID { get; set; }

        [Ignore]
        public double? Surface { get; set; }

        [Ignore]
        public int? UtilisationCapacity { get; set; }

        //number of chairs for students
        [Ignore]
        public int? OccupationCapacity { get; set; }        //1 or 0: 1 can be rostered, 0 cannot
        public String ConstructionPlanCode { get; set; }    //the id on the constructionplan
        public String IMKey { get; set; }
        public Boolean Rosterable { get; set; }
        public String OccupantName { get; set; }
        public String Telephone { get; set; }
        public String Department { get; set; }
        public String Projector { get; set; }
        public String Boards { get; set; }
        public String Equipment1 { get; set; }
        public String Equipment2 { get; set; }
        public String Curtains { get; set; }
        public String Furniture { get; set; }
        public String Facilities { get; set; }
        public String FloorMaterial { get; set; }

        [Ignore]
        public Floor Floor { get; set; }

        public override string ToString()
        {
            return UDESC + " / " + Number;
        }

    }
}
