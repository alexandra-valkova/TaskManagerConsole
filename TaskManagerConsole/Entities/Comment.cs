using System;

namespace TaskManagerConsole.Entities
{
    class Comment : BaseEntity
    {
        public int TaskID { get; set; }

        public int UserID { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }
    }
}