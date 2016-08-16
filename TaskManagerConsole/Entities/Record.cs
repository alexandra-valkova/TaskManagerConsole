using System;

namespace TaskManagerConsole.Entities
{
    public class Record : BaseEntity
    {
        public int TaskID { get; set; }

        public int UserID { get; set; }

        public int WorkingHours { get; set; }

        public DateTime CreateDate { get; set; }
    }
}