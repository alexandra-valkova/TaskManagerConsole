﻿using System.Collections.Generic;
using System.IO;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        public virtual string FilePath { get; set; }

        public BaseRepository()
        {
        }

        public virtual List<T> GetAll()
        {
            List<T> entities = new List<T>();

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    T entity = GetEntity(sr);
                    entities.Add(entity);
                }
            }

            return entities;
        }

        public virtual T GetByID(int id)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    T entity = GetEntity(sr);

                    if (entity.ID == id)
                    {
                        return entity;
                    }
                }

                return null;
            }
        }

        public int GetNextID()
        {
            int id = 0;

            using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    T entity = GetEntity(sr);

                    if (id < entity.ID)
                    {
                        id = entity.ID;
                    }
                }

                id++;
                return id;
            }
        }

        public void Add(T item)
        {
            item.ID = GetNextID();

            using (FileStream fs = new FileStream(FilePath, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                SaveEntity(item, sw);
            }
        }

        public void Edit(T item)
        {
            string tempFile = "temp." + FilePath;

            using (FileStream ifs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(ifs))
            using (FileStream ofs = new FileStream(tempFile, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(ofs))
            {
                while (!sr.EndOfStream)
                {
                    T entity = GetEntity(sr);

                    if (item.ID == entity.ID)
                    {
                        SaveEntity(item, sw);
                    }

                    else
                    {
                        SaveEntity(entity, sw);
                    }
                }
            }

            File.Delete(FilePath);
            File.Move(tempFile, FilePath);
        }

        public void Delete(T item)
        {
            string tempFile = "temp." + FilePath;

            using (FileStream ifs = new FileStream(FilePath, FileMode.OpenOrCreate))
            using (StreamReader sr = new StreamReader(ifs))
            using (FileStream ofs = new FileStream(tempFile, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(ofs))
            {
                while (!sr.EndOfStream)
                {
                    T entity = GetEntity(sr);

                    if (item.ID != entity.ID)
                    {
                        SaveEntity(entity, sw);
                    }
                }
            }

            File.Delete(FilePath);
            File.Move(tempFile, FilePath);
        }

        public void Save(T entity)
        {
            if (entity.ID == 0)
            {
                Add(entity);
            }

            else
            {
                Edit(entity);
            }
        }

        //Abstract Methods

        protected abstract void SaveEntity(T entity, StreamWriter sw);

        protected abstract T GetEntity(StreamReader sr);
    }
}