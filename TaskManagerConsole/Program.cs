using TaskManagerConsole.Services;
using TaskManagerConsole.Views;

namespace TaskManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginView login = new LoginView();
            login.Show();

            if (AuthenticationService.LoggedUser.IsAdmin == true)
            {
                AdminView admin = new AdminView();
                admin.Show();
                string choice = admin.Choice();

                if (choice == "U")
                {
                    UsersManagementView usersView = new UsersManagementView();
                    usersView.Show();
                }

                else if (choice == "T")
                {
                    TasksManagementView tasks = new TasksManagementView();
                    tasks.Show();
                }
            }

            else
            {
                TasksManagementView tasks = new TasksManagementView();
                tasks.Show();
            }
        }
    }
}