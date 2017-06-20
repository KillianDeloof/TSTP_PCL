using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSTP_PCL.Models
{
    public class AuthenticatedUser : BaseItem
    {
        public string Token { get; set; }
    }
}
