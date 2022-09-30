using System;
using System.Collections.Generic;
using System.IO;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    class RecordRepository : BaseRepository<Record>
    {
        public override string FilePath
        {
            get
            {
                return base.FilePath = "records.txt";
            }
        }

        public RecordRepository()
        {
        }

        protected override Record GetEntity(StreamReader sr)
        {
            Record record = new Record
            {
                ID = int.Parse(sr.ReadLine()),
                TaskID = int.Parse(sr.ReadLine()),
                UserID = int.Parse(sr.ReadLine()),
                WorkingHours = int.Parse(sr.ReadLine()),
                CreateDate = DateTime.Parse(sr.ReadLine())
            };

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

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
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