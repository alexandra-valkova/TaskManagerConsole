using System;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Views
{
    public class TasksManagementView : BaseView<Task>
    {
        public override Task entity { get; set; }

        protected override BaseRepository<Task> GetRepo()
        {
            TaskRepository taskRepo = new TaskRepository();
            return taskRepo;
        }

        protected override Task GetEntity(Task task)
        {
            Console.Write("Title: ");
            task.Title = Console.ReadLine();

            Console.Write("Description: ");
            task.Description = Console.ReadLine();

            Console.Write("Working Hours: ");
            task.WorkingHours = int.Parse(Console.ReadLine());

            task.CreatorID = AuthenticationService.LoggedUser.ID;

            Console.Write("Responsible ID: ");
            task.ResponsibleID = int.Parse(Console.ReadLine());

            task.CreateDate = DateTime.Now;

            task.LastEditDate = DateTime.Now;

            Console.Write("Status (InProgress or Finished): ");
            task.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), Console.ReadLine());

            return task;
        }

        protected override void RenderEntity(Task task)
        {
            Console.WriteLine("ID: " + task.ID);
            Console.WriteLine("Title: " + task.Title);
            Console.WriteLine("Description: " + task.Description);
            Console.WriteLine("Working Hours: " + task.WorkingHours);
            Console.WriteLine("Creator ID: " + task.CreatorID);
            Console.WriteLine("Responsible ID: " + task.ResponsibleID);
            Console.WriteLine("Created Date: " + task.CreateDate);
            Console.WriteLine("Last Edit Date: " + task.LastEditDate);
            Console.WriteLine("Status: " + task.Status);

            Console.WriteLine("########################################");
        }

        protected override Task EditEntity(Task task)
        {
            Console.WriteLine("Title: " + task.Title);
            Console.Write("New Title: ");
            task.Title = Console.ReadLine();

            Console.WriteLine("Description: " + task.Description);
            Console.Write("New Description: ");
            task.Description = Console.ReadLine();

            Console.WriteLine("Working Hours: " + task.WorkingHours);
            Console.Write("New Working Hours: ");
            task.WorkingHours = int.Parse(Console.ReadLine());

            task.LastEditDate = DateTime.Now;

            Console.WriteLine("Status: " + task.Status);
            Console.Write("New Status: ");
            task.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), Console.ReadLine());

            return task;
        }

        protected override void View()
        {
            base.View();

            TasksDetailsView taskDetails = new TasksDetailsView(this.entity);
            taskDetails.Show();
        }
    }
}