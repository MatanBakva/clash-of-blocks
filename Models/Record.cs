using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;


namespace Clash_Of_Blocks.Droid
{
    [Table("tblRecord")]
    class Record
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Score { set; get; }
        public int Level { get; set; }

        public Record()
        {
        }

        public Record(string name, double score, int level)
        {
            Name = name;
            Date = DateTime.Now;
            Score = score;
            Level = level;
        }

        public static void AddRecord(string name, double score, int level)
        {
            Record r = new Record(name, score, level);
            SqlHelper.GetConnection().Insert(r);
        }

        public static async Task<bool> AddRecordFirebase(string name, double score, int level)
        {
            try
            {
                Record record = new Record(name, score, level);
                await FireBaseHelper.RecordsCollection.AddDocumentAsync(record);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<List<Record>> ReverseSortForLvl(int level)
        {
            var query = await FireBaseHelper.RecordsCollection.WhereEqualsTo("Level", level).GetDocumentsAsync();
            List<Record> records = query.ToObjects<Record>().ToList();
            return records.OrderByDescending((rec) => rec.Score).ToList();
        }

        public static List<Record> GetAllRecords(int level)
        {
            string Query = string.Format("SELECT tblRecord.* FROM tblRecord WHERE (((tblRecord.Level)=\"{0}\")) ORDER BY tblRecord.Score DESC;", level);
            List<Record> Allrecords = SqlHelper.GetConnection().Query<Record>(Query);
            return Allrecords;
        }

        public static Record GetRecord(string name)
        {
            string Query = string.Format("SELECT tblRecord.* FROM tblRecord WHERE (((tblRecord.Name)=\"{0}\"));", name);
            List<Record> Records = SqlHelper.GetConnection().Query<Record>(Query);
            if (Records.Count == 1)
            {
                return Records[0];
            }
            return null;
        }
    }
}