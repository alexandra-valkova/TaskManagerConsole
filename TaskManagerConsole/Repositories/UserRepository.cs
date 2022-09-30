using System;
using System.IO;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public override string FilePath
        {
            get
            {
                return base.FilePath = "users.txt";
            }
        }

        public UserRepository()
        {
        }

        protected override User GetEntity(StreamReader sr)
        {
            User user = new User
            {
                ID = int.Parse(sr.ReadLine()),
                Username = sr.ReadLine(),
                Password = sr.ReadLine(),
                IsAdmin = Convert.ToBoolean(sr.ReadLine())
            };

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
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
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