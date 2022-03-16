using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using MvvmHelpers;

namespace S7IOTester.Models
{
    [Table("IOTemplates")]
    public class IOTemplates
    {
        [Indexed]
        [Column("cpus_name")]
        public string CPUsName { get; set; }

        [Column("address")]
        public int Address { get; set; }

        [PrimaryKey]
        [Column("name")]
        public string Name { get; set; }

    }

    [Table("CPUs")]
    public class CPUs
    {
        [PrimaryKey]
        [Column("name")]
        public string Name { get; set; }

        [Column("Family")]
        public string Family { get; set; }

        [Column("IP")]
        public string IP { get; set; }

        

        

    }

    public class DatabaseHandler
    {

        private SQLiteConnection _db;

        public DatabaseHandler()
        {
            string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "SQLiteDb.db3");

            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<IOTemplates>();
            _db.CreateTable<CPUs>();
        }

        //Insert new variable template to DB
        public void InsertTemplate(IOTemplates iotemplate)
        {
            _db.Insert(iotemplate);
        }

        //Insert new CPU to DB
        public void InsertCPU(CPUs cpu)
        {
            var query = _db.Table<CPUs>().Where(x => x.Name == cpu.Name);

            try
            {
                _db.Insert(cpu);
            }
            catch (Exception)
            {
                throw new DatabaseHandlerException("CPU name already exists!");
            }
            

        }

        public List<CPUs> SelectCPUs()
        {
            var results = _db.Table<CPUs>().ToList();
            return results;
        }

    }

    class DatabaseHandlerException : Exception
    {
        public DatabaseHandlerException() : base() { }
        public DatabaseHandlerException(string message) : base(message) { }

    }




}
