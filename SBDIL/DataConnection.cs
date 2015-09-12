using SBDIR.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBDIR
{
    public class DataConnection
    {
        public DataConnection()
        {
            _dbPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "App_Data"), "SBDIL.db");
        }

        public void InsertLog(string offender)
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                connection.Insert(new Log { Offender = offender, TimeStamp = DateTime.Now });
            }
        }

        public List<Log> GetLogs()
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                return connection.Table<Log>().ToList();
            }
        }

        public void CreateTables()
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                connection.CreateTable<Log>();
            }
        }


        private string _dbPath;
    }
}
