using SBDIL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBDIL
{
    public class DataConnection
    {
        public DataConnection()
        {
            _dbPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "db"), "SBDIL.db");

            CreateTables();
            /*
             * 
    func tableExists(tableName: String) -> Bool {
        return db.scalar(
            "SELECT EXISTS(SELECT name FROM sqlite_master WHERE name = ?)", tableName
        ) as Bool
    }
             * */

        }
        public void InsertOffender(Offender offender)
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                connection.Insert(offender);
            }
        }
        public void InsertLog(Log log)
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                connection.Insert(log);
            }
        }

        public void InsertRecording(Recording recording)
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                connection.Insert(recording);
            }
        }

        public List<Log> GetLogs()
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                return connection.Table<Log>().ToList();
            }
        }

        public List<Recording> GetRecordings()
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                return connection.Table<Recording>().ToList();
            }
        }

        public void CreateTables()
        {
            using (var connection = new SQLiteConnection(_dbPath) { Trace = true })
            {
                connection.CreateTable<Recording>();
                connection.CreateTable<Offender>();
                connection.CreateTable<Log>();
            }
        }


        private string _dbPath;
    }
}
