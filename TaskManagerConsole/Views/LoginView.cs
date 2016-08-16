using System;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Views
{
    public class LoginView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Login");

                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                AuthenticationService.Login(username, password);

                if (AuthenticationService.LoggedUser != null)
                {
                    Console.WriteLine("Welcome " + AuthenticationService.LoggedUser.Username);
                    Console.ReadKey(true);
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid username or password!");
                    Console.ReadKey(true);
                    continue;
                }
            }
        }
    }
}