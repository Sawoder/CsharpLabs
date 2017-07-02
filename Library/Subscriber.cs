using System;
using System.Collections.Generic;

namespace LibraryApp
{
    public class Subscriber
    {
        public string Name { get; }
        public string Phone { get; }

        public List<Book> Books { get; set; }
        public bool HasRarityBook { get; set; }

        public Subscriber(string Name, string Phone)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Books = new List<Book>();
            this.HasRarityBook = false;
        }

        public Book this[int pos]
        {
            get {
                try { return ListBooks()[pos]; }
                catch (ArgumentOutOfRangeException) { return null; }
            }
        }

        // Список книг
        public List<Book> ListBooks()
        {
            return Books;
        }

        // Список просроченных книг
        public List<Book> OverdueBooks()
        {
            List<Book> ob = new List<Book>();
            foreach (Book book in Books)
            {
                if (book.OverdueBook())
                    ob.Add(book);
            } 
            return ob;
        }

        // Получение книги
        public void TakeBook(Library library, Book book)
        {
            library.GiveBook(this, book);
        }

        // Возврат книги
        public void ReturnBook(Library library, Book book)
        {
            book.ReturnBook(library, this);
        }

    }
}
