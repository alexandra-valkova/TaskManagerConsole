using System;
using System.Collections.Generic;
using System.IO;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Repositories
{
    public class TaskRepository : BaseRepository<Task>
    {
        public override string filePath
        {
            get
            {
                return base.filePath = "tasks.txt";
            }
        }

        public TaskRepository()
        {
        }

        protected override Task GetEntity(StreamReader sr)
        {
            Task task = new Task();

            task.ID = int.Parse(sr.ReadLine());
            task.Title = sr.ReadLine();
            task.Description = sr.ReadLine();
            task.WorkingHours = int.Parse(sr.ReadLine());
            task.CreatorID = int.Parse(sr.ReadLine());
            task.ResponsibleID = int.Parse(sr.ReadLine());
            task.CreateDate = DateTime.Parse(sr.ReadLine());
            task.LastEditDate = DateTime.Parse(sr.ReadLine());
            task.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), sr.ReadLine());

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

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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