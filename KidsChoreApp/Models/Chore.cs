﻿using SQLite;


namespace KidsChoreApp.Models
{
    public class Chore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ChildId { get; set; } // Foreign key to Child
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Worth { get; set; }
        public int Priority { get; set; }
        public bool IsComplete { get; set; }
    }
}
