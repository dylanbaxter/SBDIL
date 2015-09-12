using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDIR.Models
{
    public class Log
    {
        [PrimaryKey, AutoIncrement]
        public int LogId { get; set; }

        public string Offender { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
