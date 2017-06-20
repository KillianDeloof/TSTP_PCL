using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TSTP_PCL.Models
{
    public class MainCategory
    {
        public int ID { get; set; }

        public int CategoryId { get; set; }
        public string CategoryUCode { get; set; }
        public string CategoryUDesc { get; set; }
        public string Picture { get; set; }
        public string Subtitle { get; set; }
        public List<Category> SubCategoryList { get; set; }

        public override string ToString()
        {
            return CategoryUDesc;
        }
    }
}
