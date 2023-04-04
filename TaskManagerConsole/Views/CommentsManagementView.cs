using System;
using System.Collections.Generic;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Views
{
    class CommentsManagementView
    {
        public Task task;

        public CommentsManagementView(Task task)
        {
            this.task = task;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Comments");

            Console.WriteLine("[L]ist all comments:");
            Console.WriteLine("[A]dd comment:");

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

            List<Comment> comments = new List<Comment>();

            CommentRepository commentRepo = new CommentRepository();
            comments = commentRepo.GetAll(task.ID);

            foreach (Comment comment in comments)
            {
                Console.WriteLine("Text: " + comment.Text);
                Console.WriteLine("Created Date: " + comment.CreateDate);
            }

            Console.ReadKey(true);
        }

        private void Add()
        {
            Console.Clear();

            Comment comment = new Comment
            {
                TaskID = task.ID,
                UserID = AuthenticationService.LoggedUser.ID
            };

            Console.Write("Text: ");
            comment.Text = Console.ReadLine();

            comment.CreateDate = DateTime.Now;

            CommentRepository commentRepo = new CommentRepository();
            commentRepo.Add(comment);

            Console.WriteLine("Comment successfully added!");
            Console.ReadKey(true);
        }
    }
}