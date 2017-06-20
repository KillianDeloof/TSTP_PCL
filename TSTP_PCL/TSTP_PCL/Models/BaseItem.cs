using SQLite;

namespace TSTP_PCL.Models
{
    public class BaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}