using System;

namespace TaskManagerConsole.Views
{
    public class AdminView
    {
        public void Show()
        {
            Console.Clear();

            Console.WriteLine("[U]sers Management");
            Console.WriteLine("[T]asks Management");
        }

        public string Choice()
        {
            string choice;
            while (true)
            {
                choice = Console.ReadLine().ToUpper();

                if (choice == "U" || choice == "T")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }

            return choice;
        }
    }
}