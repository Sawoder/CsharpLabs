using System;
using System.Collections.Generic;

namespace LibraryApp
{
    public class Book
    {
        public string Author { get; }
        public string Name { get; }
        public bool IsRarity { get; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Subscriber Sub { get; set; }
        public bool IsOverdue { get; set; }

        
        public Book(string Author, string Name, bool IsRarity)
        {
            this.Author = Author;
            this.Name = Name;
            this.IsRarity = IsRarity;
            this.Sub = null;
            this.Begin = DateTime.MinValue;
            this.End = DateTime.MinValue;
            this.IsOverdue = false;
        }

        public string WhereAreBook()
        {
            if (Sub == null)
                return "Library";
            else
                return Sub.Name;
        }

        public bool OverdueBook()
        {
            if (End.DayOfYear - Begin.DayOfYear >= 14)
            {
                IsOverdue = true;
                return true;
            }
            else
            {
                IsOverdue = false;
                return false;
            }
        }

        public DateTime DateOfIssue()
        {
            return this.Begin;
        }
    }
}
