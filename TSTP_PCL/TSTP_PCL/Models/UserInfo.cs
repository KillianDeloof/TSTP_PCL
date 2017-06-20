using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TSTP_PCL.Models
{
    public class UserInfo : BaseItem
    {

        public String FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Ignore]
        public List<String> Roles { get; set; } = new List<string>();

        public string FirstRole { get; set; }



        public String KernelTeamCCode { get; set; }

        [Ignore]
        public List<String> EducationCodes { get; set; } = new List<string>();


        //public DateTime ExpireDate { get; set; }
        public override string ToString()
        {
            return FirstName;
        }
    }
}
