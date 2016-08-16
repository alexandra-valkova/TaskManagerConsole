using System;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Views
{
    public class UsersManagementView : BaseView<User>
    {
        protected override BaseRepository<User> GetRepo()
        {
            UserRepository userRepo = new UserRepository();
            return userRepo;
        }

        protected override User EditEntity(User user)
        {
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

            return user;
        }

        protected override User GetEntity(User user)
        {
            Console.Write("Username: ");
            user.Username = Console.ReadLine();

            Console.Write("Password: ");
            user.Password = Console.ReadLine();

            Console.Write("Is admin: ");

            user.IsAdmin = Convert.ToBoolean(Console.ReadLine());

            return user;
        }

        protected override void RenderEntity(User user)
        {
            Console.WriteLine("ID: " + user.ID);
            Console.WriteLine("Username: " + user.Username);
            Console.WriteLine("Is admin: " + user.IsAdmin);

            Console.WriteLine("########################################");
        }
    }
}