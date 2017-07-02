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
        public Subscriber Sub { get; set; }

        
        public Book(string Author, string Name, bool IsRarity)
        {
            this.Author = Author;
            this.Name = Name;
            this.IsRarity = IsRarity;
            this.Sub = null;
            this.Begin = DateTime.MinValue;
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
            if (DateTime.Now.DayOfYear - Begin.DayOfYear >= 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReturnBook(Library library, Subscriber sub)
        {
            sub.Books.Remove(this);
            Begin = DateTime.MinValue;
            OverdueBook();
            Sub = null;
            if (IsRarity)
                sub.HasRarityBook = false;
            library.AddBook(this);
        }

        public DateTime DateOfIssue()
        {
            return this.Begin;
        }
    }
}
