using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDIL.Models
{
    public class Log
    {
        [PrimaryKey, AutoIncrement]
        public int LogId { get; set; }

        [ForeignKey(typeof(Offender))]
        public int OffenderId { get; set; }

        public DateTime TimeStamp { get; set; }

        public double NoiseLevel { get; set; }

        [ManyToOne]
        public Offender Offender { get; set; }

        public Log(Offender offender, double noiseLevel)
        {
            Offender = offender;
            NoiseLevel = noiseLevel;
            TimeStamp = DateTime.Now;
        }

        public Log() {}
    }


    public class Offender
    {
        [PrimaryKey, AutoIncrement]
        public int OffenderId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Offender() { }
    }


    public class Recording
    {
        [PrimaryKey, AutoIncrement]
        public int RecordingId { get; set; }

        public string FileName { get; set; }

        public Recording(string fileName)
        {
            FileName = fileName;
        }

        public Recording() { }
    }
}
