using System;
using System.Collections.Generic;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Views
{
    public abstract class BaseView<T> where T : BaseEntity, new()
    {
        public virtual T Entity { get; set; }

        public void Show()
        {
            while (true)
            {
                Menu choice = RenderMenu();

                switch (choice)
                {
                    case Menu.List:
                        List();
                        break;
                    case Menu.View:
                        View();
                        break;
                    case Menu.Add:
                        Add();
                        break;
                    case Menu.Edit:
                        Edit();
                        break;
                    case Menu.Delete:
                        Delete();
                        break;
                    case Menu.Exit:
                        return;
                }
            }
        }

        protected void List()
        {
            Console.Clear();

            BaseRepository<T> entityRepo = GetRepo();

            List<T> entities = entityRepo.GetAll();

            foreach (T entity in entities)
            {
                RenderEntity(entity);
            }

            Console.ReadKey();
        }

        protected virtual void View()
        {
            Console.Clear();

            Console.Write(typeof(T).Name + " ID: ");

            while (true)
            {
                int entityID = int.Parse(Console.ReadLine());

                BaseRepository<T> entityRepo = GetRepo();
                T entity = entityRepo.GetByID(entityID);

                if (entity == null)
                {
                    Console.WriteLine(typeof(T).Name + " not found!");
                }

                else
                {
                    RenderEntity(entity);
                    Entity = entity;
                    break;
                }
            }
        }

        protected void Add()
        {
            Console.Clear();

            T entity = new T();

            Console.WriteLine("Add new " + typeof(T).Name);

            entity = GetEntity(entity);

            BaseRepository<T> entityRepo = GetRepo();
            entityRepo.Save(entity);

            Console.WriteLine(typeof(T).Name + " successfully added!");
            Console.ReadKey(true);
        }

        protected void Edit()
        {
            Console.Clear();

            BaseRepository<T> entityRepo = GetRepo();

            Console.Write(typeof(T).Name + " ID: ");

            while (true)
            {
                int entityID = int.Parse(Console.ReadLine());

                T entity = entityRepo.GetByID(entityID);

                if (entity == null)
                {
                    Console.WriteLine(typeof(T).Name + " not found!");
                }

                else
                {
                    entity = EditEntity(entity);
                    entityRepo.Save(entity);
                    Console.WriteLine(typeof(T).Name + " successfully edited!");
                    break;
                }
            }

            Console.ReadKey(true);
        }

        protected void Delete()
        {
            Console.Clear();

            BaseRepository<T> entityRepo = GetRepo();

            Console.Write("Delete " + typeof(T).Name + " ID: ");
            int entityID = int.Parse(Console.ReadLine());

            T entity = entityRepo.GetByID(entityID);

            if (entity == null)
            {
                Console.WriteLine(typeof(T).Name + " not found!");
            }

            else
            {
                entityRepo.Delete(entity);
                Console.WriteLine(typeof(T).Name + " successfully deleted!");
            }

            Console.ReadKey(true);
        }

        public Menu RenderMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(typeof(T).Name + " Management");

                Console.WriteLine("[L]ist all:");
                Console.WriteLine("[V]iew:");
                Console.WriteLine("[A]dd:");
                Console.WriteLine("[E]dit:");
                Console.WriteLine("[D]elete:");
                Console.WriteLine("E[x]it:");

                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "L":
                        return Menu.List;
                    case "V":
                        return Menu.View;
                    case "A":
                        return Menu.Add;
                    case "E":
                        return Menu.Edit;
                    case "D":
                        return Menu.Delete;
                    case "X":
                        return Menu.Exit;
                    default:
                        Console.WriteLine("Invalid operation!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        protected abstract BaseRepository<T> GetRepo();

        protected abstract void RenderEntity(T entity);

        protected abstract T GetEntity(T entity);

        protected abstract T EditEntity(T entity);
    }

    public enum Menu
    {
        List = 1,
        View = 2,
        Add = 3,
        Edit = 4,
        Delete = 5,
        Exit = 6
    }
}