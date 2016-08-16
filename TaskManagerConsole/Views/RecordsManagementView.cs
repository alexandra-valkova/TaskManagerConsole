using System;
using System.Collections.Generic;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Views
{
    class RecordsManagementView
    {
        public Task task;

        public RecordsManagementView(Task task)
        {
            this.task = task;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Records");

            Console.WriteLine("[L]ist all records");
            Console.WriteLine("[A]dd record");

            while (true)
            {
                string choice = Console.ReadLine().ToUpper();

                if (choice == "L")
                {
                    List();
                    break;
                }

                else if (choice == "A")
                {
                    Add();
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid choice!");
                } 
            }
        }

        private void List()
        {
            Console.Clear();

            List<Record> records = new List<Record>();

            RecordRepository recordRepo = new RecordRepository();
            records = recordRepo.GetAll(task.ID);

            foreach (Record record in records)
            {
                Console.WriteLine("Working Hours: " + record.WorkingHours);
                Console.WriteLine("Created Date: " + record.CreateDate);
            }

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Record record = new Record();

            record.TaskID = task.ID;
            record.UserID = AuthenticationService.LoggedUser.ID;

            Console.Write("Working Hours: ");
            record.WorkingHours = int.Parse(Console.ReadLine());

            record.CreateDate = DateTime.Now;

            RecordRepository recordRepo = new RecordRepository();
            recordRepo.Add(record);

            Console.WriteLine("Record successfully added!");
            Console.ReadKey(true);
        }
    }
}