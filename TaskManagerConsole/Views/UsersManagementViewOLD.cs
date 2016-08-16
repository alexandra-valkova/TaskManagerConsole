using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Enumerators;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Views
{
    class UsersManagementView
    {
        public void Show()
        {
            while (true)
            {
                UsersMenu choice = RenderMenu();

                switch (choice)
                {
                    case UsersMenu.List:
                        List();
                        break;
                    case UsersMenu.View:
                        View();
                        break;
                    case UsersMenu.Add:
                        Add();
                        break;
                    case UsersMenu.Edit:
                        Edit();
                        break;
                    case UsersMenu.Delete:
                        Delete();
                        break;
                    case UsersMenu.Exit:
                        return;
                }
            }
        }

        public UsersMenu RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Users Management");

                Console.WriteLine("[L]ist all users:");
                Console.WriteLine("[V]iew user:");
                Console.WriteLine("[A]dd user:");
                Console.WriteLine("[E]dit user:");
                Console.WriteLine("[D]elete user:");
                Console.WriteLine("E[x]it:");

                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "L":
                        return UsersMenu.List;
                    case "V":
                        return UsersMenu.View;
                    case "A":
                        return UsersMenu.Add;
                    case "E":
                        return UsersMenu.Edit;
                    case "D":
                        return UsersMenu.Delete;
                    case "X":
                        return UsersMenu.Exit;
                    default:
                        Console.WriteLine("Invalid operation!");
                        Console.ReadKey();
                        break;
                } 
            }
        }

        public void List()
        {
            Console.Clear();

            UserRepository userRepo = new UserRepository();

            List<User> users = userRepo.GetAll();

            foreach (User user in users)
            {
                Console.WriteLine("ID: " + user.ID);
                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("Is admin: " + user.IsAdmin);

                Console.WriteLine("########################################");
            }

            Console.ReadKey(true);
        }

        public void View()
        {
            Console.Clear();

            Console.Write("User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetByID(userId);

            if (user == null)
            {
                Console.WriteLine("User not found!");
                Console.ReadKey(true);
                return;
            }

            else
            {
                Console.WriteLine("ID: " + user.ID);
                Console.WriteLine("Username: " + user.Username);
                Console.WriteLine("Is admin: " + user.IsAdmin);
                Console.ReadKey(true);
            }
        }

        public void Add()
        {
            Console.Clear();

            User user = new User();

            Console.WriteLine("Add new user");

            Console.Write("Username: ");
            user.Username = Console.ReadLine();

            Console.Write("Password: ");
            user.Password = Console.ReadLine();

            Console.Write("Is admin: ");

            user.IsAdmin = Convert.ToBoolean(Console.ReadLine());

            UserRepository userRepo = new UserRepository();
            userRepo.Save(user);

            Console.WriteLine("User saved successfully!");
            Console.ReadKey(true);
        }

        public void Edit()
        {
            Console.Clear();

            Console.Write("User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetByID(userId);

            if (user == null)
            {
                Console.WriteLine("User not found!");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("Editing User: " + user.Username);
            Console.WriteLine("ID: " + user.ID);

            Console.WriteLine("Username: " + user.Username);
            Console.Write("New Username: ");
            user.Username = Console.ReadLine();

            Console.WriteLine("Password: " + user.Password);
            Console.Write("New Password: ");        
            user.Password = Console.ReadLine();

            Console.WriteLine("Is admin: " + user.IsAdmin);
            Console.Write("New Is admin: ");
            user.IsAdmin = Convert.ToBoolean(Console.ReadLine());

            userRepo.Save(user);

            Console.WriteLine("User saved successfully!");
            Console.ReadKey(true);
        }

        public void Delete()
        {
            Console.Clear();

            UserRepository userRepo = new UserRepository();

            Console.WriteLine("Delete User:");
            Console.Write("User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            User user = userRepo.GetByID(userId);

            if (user == null)
            {
                Console.WriteLine("User not found!");
            }

            else
            {
                userRepo.Delete(user);
                Console.WriteLine("User deleted successfully!");
            }

            Console.ReadKey(true);
        }
    }
}
