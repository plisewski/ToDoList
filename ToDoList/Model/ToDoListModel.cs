using System;

namespace ToDoList.Model
{
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public DateTime Deadline { get; set; }
    }
}