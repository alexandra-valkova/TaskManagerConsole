using System;
using System.IO;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public override string filePath
        {
            get
            {
                return base.filePath = "users.txt";
            }
        }

        public UserRepository()
        {
        }

        protected override User GetEntity(StreamReader sr)
        {
            User user = new User();
            user.ID = int.Parse(sr.ReadLine());
            user.Username = sr.ReadLine();
            user.Password = sr.ReadLine();
            user.IsAdmin = Convert.ToBoolean(sr.ReadLine());
            return user;
        }

        protected override void SaveEntity(User user, StreamWriter sw)
        {
            sw.WriteLine(user.ID);
            sw.WriteLine(user.Username);
            sw.WriteLine(user.Password);
            sw.WriteLine(user.IsAdmin);
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    User user = GetEntity(sr);

                    if (username == user.Username && password == user.Password)
                    {
                        return user;
                    }
                }

                return null;
            }
        }
    }
}