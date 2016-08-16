using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Enumerators;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Views
{
    class TasksManagementView
    {
        public void Show()
        {
            while (true)
            {
                TasksMenu choice = RenderMenu();

                switch (choice)
                {
                    case TasksMenu.List:
                        List();
                        break;
                    case TasksMenu.View:
                        View();
                        break;
                    case TasksMenu.Add:
                        Add();
                        break;
                    case TasksMenu.Edit:
                        Edit();
                        break;
                    case TasksMenu.Delete:
                        Delete();
                        break;
                    case TasksMenu.Exit:
                        return;
                }
            }
        }

        private void List()
        {
            Console.Clear();

            TaskRepository taskRepo = new TaskRepository();
            List<Task> tasks = taskRepo.GetAll();

            foreach (Task task in tasks)
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

            Console.ReadKey(true);
        }

        private void View()
        {
            Console.Clear();
            Console.Write("Task ID: ");
            int id = int.Parse(Console.ReadLine());

            TaskRepository taskRepo = new TaskRepository();

            Task task = taskRepo.GetByID(id);

            if (task == null)
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey(true);
                return;
            }

            else if (task.CreatorID == AuthenticationService.LoggedUser.ID || task.ResponsibleID == AuthenticationService.LoggedUser.ID)
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

                TasksDetailsView taskDetails = new TasksDetailsView(entity);
                taskDetails.Show();
            }

            else
            {
                Console.WriteLine("You are not allowed to view this task!");
                Console.ReadKey(true);
                return;
            }

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Task task = new Task();

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

            TaskRepository taskRepo = new TaskRepository();
            taskRepo.Save(task);

            Console.WriteLine("Task successfully added!");
            Console.ReadKey(true);
        }

        private void Edit()
        {
            Console.Clear();

            Console.Write("Task ID: ");
            int id = int.Parse(Console.ReadLine());

            TaskRepository taskRepo = new TaskRepository();
            Task task = taskRepo.GetByID(id);

            if (task == null)
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey(true);
                return;
            }

            else if (task.CreatorID == AuthenticationService.LoggedUser.ID || task.ResponsibleID == AuthenticationService.LoggedUser.ID)
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

                taskRepo.Save(task);
                Console.WriteLine("Task successfully edited!");
            }

            else
            {
                Console.WriteLine("You are not allowed to edit this task!");
                Console.ReadKey(true);
                return;
            }

            Console.ReadKey(true);
        }

        private void Delete()
        {
            Console.Clear();

            Console.Write("Task ID: ");
            int id = int.Parse(Console.ReadLine());

            TaskRepository taskRepo = new TaskRepository();
            Task task = taskRepo.GetByID(id);

            if (task == null)
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey(true);
                return;
            }

            else if (task.CreatorID == AuthenticationService.LoggedUser.ID || task.ResponsibleID == AuthenticationService.LoggedUser.ID)
            {
                taskRepo.Delete(task);

                Console.WriteLine("Task successfully deleted!"); 
            }

            else
            {
                Console.WriteLine("You are not allowed to delete this task!");
                Console.ReadKey(true);
                return;
            }

            Console.ReadKey(true);
        }

        public TasksMenu RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Tasks Management");

                Console.WriteLine("[L]ist all tasks:");
                Console.WriteLine("[V]iew task:");
                Console.WriteLine("[A]dd task:");
                Console.WriteLine("[E]dit task:");
                Console.WriteLine("[D]elete task:");
                Console.WriteLine("E[x]it");

                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "L":
                        return TasksMenu.List;
                    case "V":
                        return TasksMenu.View;
                    case "A":
                        return TasksMenu.Add;
                    case "E":
                        return TasksMenu.Edit;
                    case "D":
                        return TasksMenu.Delete;
                    case "X":
                        return TasksMenu.Exit;
                    default:
                        Console.WriteLine("Inavlid choice!");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}
