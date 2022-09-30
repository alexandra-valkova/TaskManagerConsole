using System;
using System.Collections.Generic;
using System.IO;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Repositories
{
    public class TaskRepository : BaseRepository<Task>
    {
        public override string FilePath
        {
            get
            {
                return base.FilePath = "tasks.txt";
            }
        }

        public TaskRepository()
        {
        }

        protected override Task GetEntity(StreamReader sr)
        {
            Task task = new Task
            {
                ID = int.Parse(sr.ReadLine()),
                Title = sr.ReadLine(),
                Description = sr.ReadLine(),
                WorkingHours = int.Parse(sr.ReadLine()),
                CreatorID = int.Parse(sr.ReadLine()),
                ResponsibleID = int.Parse(sr.ReadLine()),
                CreateDate = DateTime.Parse(sr.ReadLine()),
                LastEditDate = DateTime.Parse(sr.ReadLine()),
                Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), sr.ReadLine())
            };

            return task;
        }

        protected override void SaveEntity(Task entity, StreamWriter sw)
        {
            sw.WriteLine(entity.ID);
            sw.WriteLine(entity.Title);
            sw.WriteLine(entity.Description);
            sw.WriteLine(entity.WorkingHours);
            sw.WriteLine(entity.CreatorID);
            sw.WriteLine(entity.ResponsibleID);
            sw.WriteLine(entity.CreateDate);
            sw.WriteLine(entity.LastEditDate);
            sw.WriteLine(entity.Status);
        }

        public override List<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    Task task = GetEntity(sr);

                    if (task.CreatorID == AuthenticationService.LoggedUser.ID || task.ResponsibleID == AuthenticationService.LoggedUser.ID)
                    {
                        tasks.Add(task);
                    }
                }

                return tasks;
            }
        }
        public override Task GetByID(int id)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    Task task = GetEntity(sr);

                    if (task.ID == id && (task.CreatorID == AuthenticationService.LoggedUser.ID || task.ResponsibleID == AuthenticationService.LoggedUser.ID))
                    {
                        return task;
                    }
                }

                return null;
            }
        }
    }
}