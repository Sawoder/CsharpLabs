using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryApp;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private Library library;
        private Subscriber subscriber;
        private Book book1;
        private Book book2;
        private Book book3;
        private Book book4;
        private Book book5;
        private Book book6;
        private Book book7;
        private Book book8;
        private Book book9;

        [TestInitialize]
        public void SetUp()
        {
            library = new Library();
            subscriber = new Subscriber("First", "123456789");

            book1 = new Book("1", "11", true);
            book2 = new Book("2", "22", false);
            book3 = new Book("3", "33", false);
            book4 = new Book("4", "44", false);
            book5 = new Book("5", "55", false);
            book6 = new Book("6", "66", false);
            book7 = new Book("7", "77", false);
            book8 = new Book("8", "88", true);
            book9 = new Book("9", "99", false);

            EventHandler<string> changeStateHandler = (sender, str) => Console.WriteLine(str);
            EventHandler<Book> addBookHandler = (sender, book) =>
            {
                Console.WriteLine($"Book Add by {sender}");
                Console.WriteLine("{0} - {1} ({2})", book.Author, book.Name, book.IsRarity ? "Rarity" : "Not rarity");
            };
            EventHandler<Subscriber> addSubHandler = (sender, sub) =>
            {
                Console.WriteLine($"Book Add by {sender}");
                Console.WriteLine("Name: {0}\nPhone: {1}", sub.Name, sub.Phone);
            };
            library.ChangeStateHandler += changeStateHandler;
            library.AddBookHandler += addBookHandler;
            library.AddSubHandler += addSubHandler;

            library.AddBook(book1);
            library.AddBook(book2);
            library.AddBook(book3);
            library.AddBook(book4);
            library.AddBook(book5);
            library.AddBook(book6);
            library.AddBook(book7);
            library.AddBook(book8);
            library.AddBook(book9);
        }

        [TestMethod]
        public void Book_WhereAreBook()
        {
            Assert.AreEqual("Library", library.ListBooks()[0].WhereAreBook());
        }

        [TestMethod]
        public void Book_OverdueBook()
        {
            foreach (Book book in library.ListBooks())
                Assert.IsTrue(book.OverdueBook());
        }

        [TestMethod]
        public void Book_DateOfIssue()
        {
            foreach (Book book in library.ListBooks())
                Assert.AreNotEqual(DateTime.Now, book.DateOfIssue());
        }

        [TestMethod]
        public void Subscriber_ListBooks()
        {
            subscriber.TakeBook(library, book1);
            subscriber.TakeBook(library, book2);
            Assert.AreEqual(2, subscriber.ListBooks().Count);
        }

        [TestMethod]
        public void Subscriber_OverdueBooks()
        {
            subscriber.TakeBook(library, book1);
            subscriber.TakeBook(library, book2);
            subscriber.TakeBook(library, book3);
            subscriber.TakeBook(library, book4);
            Assert.AreEqual(0, subscriber.OverdueBooks().Count);
        }

        [TestMethod]
        public void Subscriber_TakeBook()
        {
            subscriber.TakeBook(library, book1);
            subscriber.TakeBook(library, book2);
            subscriber.TakeBook(library, book3);
            subscriber.TakeBook(library, book4);
            foreach (Book book in subscriber.ListBooks())
                Assert.AreEqual("First", book.WhereAreBook());
        }

        [TestMethod]
        public void Subscriber_ReturnBook()
        {
            subscriber.TakeBook(library, book1);
            subscriber.TakeBook(library, book2);
            subscriber.TakeBook(library, book3);
            subscriber.TakeBook(library, book4);
            subscriber.ReturnBook(library, book1);
            foreach (Book book in subscriber.ListBooks())
                Assert.AreNotEqual("11", book.Name);
        }

        [TestMethod]
        public void Library_AddBook()
        {
            library.AddBook(new Book("Test", "Test", true));
            Assert.AreEqual("Test", library.ListBooks()[library.ListBooks().Count - 1].Name);
        }

        [TestMethod]
        public void Library_ListBooks()
        {
            foreach (Book book in library.ListBooks())
                Assert.IsInstanceOfType(new Book("Test", "Test", true), book.GetType());
        }

        [TestMethod]
        public void Library_FullList()
        {
            subscriber.TakeBook(library, book1);
            Assert.AreEqual(9, library.FullList().Count);
        }

        [TestMethod]
        public void Library_Find()
        {
            Assert.AreEqual("55", library.Find("55", true)[0].Name);
        }

        [TestMethod]
        public void Library_GiveBook()
        {
            library.GiveBook(subscriber, book1);
            Assert.AreEqual("First", book1.WhereAreBook());
        }

        [TestMethod]
        public void Subscriber_Indexator()
        {
            subscriber.TakeBook(library, book1);
            Assert.AreEqual("1", subscriber[0].Author);
        }

        [TestMethod]
        public void Library_Indexator()
        {
            Assert.AreSame(book1, library["1", "11"]);
        }

        [TestMethod]
        public void Library_AddBookHandler()
        {
            List<string> receivedEvents = new List<string>();
            Library lib = new Library();

            lib.AddBookHandler += delegate (object sender, Book book)
            {
                receivedEvents.Add(book.Name);
            };

            lib.AddBook(new Book("test1", "test1", true));
            lib.AddBook(new Book("test2", "test2", false));
            
            Assert.AreEqual(2, receivedEvents.Count);
            Assert.AreEqual("test1", receivedEvents[0]);
            Assert.AreEqual("test2", receivedEvents[1]);
        }

        [TestMethod]
        public void Library_AddSubscriberHandler()
        {
            List<string> receivedEvents = new List<string>();
            Library lib = new Library();
            lib.AddBookHandler += delegate (object sender, Book book) { };
            lib.ChangeStateHandler += delegate (object sender, string str) { };
            lib.AddSubHandler += delegate (object sender, Subscriber sub)
            {
                receivedEvents.Add(sub.Name);
            };

            Book bk1 = new Book("test1", "test1", false);
            lib.AddBook(bk1);

            lib.GiveBook(subscriber, bk1);

            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("First", receivedEvents[0]);
        }

        [TestMethod]
        public void Library_ChangeStateBookHandler()
        {
            List<string> receivedEvents = new List<string>();
            Library lib = new Library();
            lib.AddBookHandler += delegate (object sender, Book book) { };
            lib.AddSubHandler += delegate (object sender, Subscriber sub) { };
            lib.ChangeStateHandler += delegate (object sender, string str)
            {
                receivedEvents.Add(str);
            };

            Book bk1 = new Book("test1", "test1", false);
            lib.AddBook(bk1);

            lib.GiveBook(subscriber, bk1);

            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("Change State Book: test1 - test1 was given to First - 123456789", receivedEvents[0]);
        }


        [TestCleanup]
        public void TearDown()
        {
            library = null;
            subscriber = null;
            book1 = null;
            book2 = null;
            book3 = null;
            book4 = null;
            book5 = null;
            book6 = null;
            book7 = null;
            book8 = null;
            book9 = null;
        }
    }
}
