using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TSTP_PCL.Models
{
    public partial class Campus : BaseItem
    {
        [Ignore]
        public int? CampusClusterID { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// ex : BUD
        /// </summary>
        public string UCODE { get; set; }

        /// <summary>
        /// ex : campus budda
        /// </summary>
        public string UDESC { get; set; }

        /// <summary>
        /// ex : BUD
        /// </summary>
        public string CCODE { get; set; }

        /// <summary>
        /// ex : campus budda
        /// </summary>
        public string CDESC { get; set; }
        // -- uit te zoeken --//
        public String IMKey { get; set; }
        public String Picture { get; set; }


        // lat en long van de campus
        //public double[] LatLong { get; set; }


        private double[] LatLong;

        [Ignore]
        public double[] latLong
        {
            get
            {

                switch (UCODE)
                {
                    case "BUD":
                        latLong[0] = 50.83165709999999;
                        latLong[1] = 3.2639669999999796;
                        break;
                    case "EXT":
                        return null;
                    case "GKG":
                        latLong[0] = 50.815474;
                        latLong[1] = 3.2718398;
                        break;
                    case "LPS":
                        latLong[0] = 50.82469800000001;
                        latLong[1] = 3.30499599999996;
                        break;
                    case "NHS":
                        latLong[0] = 51.2067775;
                        latLong[1] = 3.2405138999999963;
                        break;
                    case "RDR":
                        latLong[0] = 50.8223525;
                        latLong[1] = 3.283895799999982;
                        break;
                    case "RSS":
                        latLong[0] = 51.1923775;
                        latLong[1] = 3.2134982000000036;
                        break;
                    case "SJS":
                        latLong[0] = 51.2152172;
                        latLong[1] = 3.2210704000000305;
                        break;
                    case "VES":
                        latLong[0] = 51.22140020000001;
                        latLong[1] = 2.9150922999999693;
                        break;
                    default:
                        return null;
                }

                return LatLong;
            }
            set { LatLong = value; }
        }


        // afstand van huidig punt tot afstand
        // wordt opgevuld in gps-repository
        [Ignore]
        public double Distance { get; set; }

        public override string ToString()
        {
            if (UCODE != null && UDESC != null)
            {
                return UCODE + " - " + UDESC.Substring(7);
            }
            else
            {
                return "error, udesc or ucode = null";
            }

        }


    }
}
