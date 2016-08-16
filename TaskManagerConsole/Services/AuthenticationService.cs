using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{
    public static class AuthenticationService
    {
        public static User LoggedUser { get; set; }

        public static void Login(string username, string password)
        {
            UserRepository userRepo = new UserRepository();

            LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
        }

        public static void Logout()
        {
            LoggedUser = null;
        }
    }
}