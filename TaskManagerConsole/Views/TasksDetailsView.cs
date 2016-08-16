using System;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Views
{
    public class TasksDetailsView
    {
        public Task task;

        public TasksDetailsView(Task task)
        {
            this.task = task;
        }

        public void Show()
        {
            Console.WriteLine("[R]ecords Management:");
            Console.WriteLine("[C]omments Management:");

            while (true)
            {
                string choice = Console.ReadLine().ToUpper();

                if (choice == "R")
                {
                    RecordsManagementView records = new RecordsManagementView(task);
                    records.Show();
                    break;
                }

                else if (choice == "C")
                {
                    CommentsManagementView comments = new CommentsManagementView(task);
                    comments.Show();
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}