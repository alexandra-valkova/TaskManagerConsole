using System;
using System.Collections.Generic;
using System.IO;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    class RecordRepository : BaseRepository<Record>
    {
        public override string filePath
        {
            get
            {
                return base.filePath = "records.txt";
            }
        }

        public RecordRepository()
        {
        }

        protected override Record GetEntity(StreamReader sr)
        {
            Record record = new Record();

            record.ID = int.Parse(sr.ReadLine());
            record.TaskID = int.Parse(sr.ReadLine());
            record.UserID = int.Parse(sr.ReadLine());
            record.WorkingHours = int.Parse(sr.ReadLine());
            record.CreateDate = DateTime.Parse(sr.ReadLine());

            return record;
        }

        protected override void SaveEntity(Record entity, StreamWriter sw)
        {
            sw.WriteLine(entity.ID);
            sw.WriteLine(entity.TaskID);
            sw.WriteLine(entity.UserID);
            sw.WriteLine(entity.WorkingHours);
            sw.WriteLine(entity.CreateDate);
        }

        public List<Record> GetAll(int taskID)
        {
            List<Record> records = new List<Record>();

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    Record record = GetEntity(sr);

                    if (record.TaskID == taskID)
                    {
                        records.Add(record); 
                    }
                }
            }

            return records;
        }
    }
}