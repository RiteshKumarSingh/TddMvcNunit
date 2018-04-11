using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using TddMvcNunit.Controllers;
using TddMvcNunit.Models;
using TddMvcNunit.test.Repository;
using Moq;

namespace TddMvcNunit.test
{
    public class TddMVCNunitTest
    {
        Book book1 = null;
        Book book2 = null;
        Book book3 = null;
        Book book4 = null;
        Book book5 = null;

        List<Book> books = null;
        DummyBooksRepository _booksRepo = null;
        UnitOfWork uow = null;
        BooksController controller = null;

        public TddMVCNunitTest()
        {
            // Lets create some sample books

            book1 = new Book { Id = 1, BookName = "Book1", Authorname = "John" };
            book2 = new Book { Id = 2, BookName = "Book2", Authorname = "Amit" };
            book3 = new Book { Id = 3, BookName = "Book3", Authorname = "San"};
            book4 = new Book { Id = 4, BookName = "Book4", Authorname = "David" };
            book5 = new Book { Id = 5, BookName = "Test5", Authorname = "Mark" };

            books = new List<Book>
           {
               book1,
               book2,
               book3,
               book4
           };

            // Lets create our dummy repository
            _booksRepo = new DummyBooksRepository(books);
            // Let us now create the Unit of work with our dummy repository
            uow = new UnitOfWork(_booksRepo);

            // Now lets create the BooksController object to test and pass our unit of work
            controller = new BooksController(uow);
        }

        [Test]
        public void Index_Returns_AllRows()
        {

            // Lets call the action method now
            var result = controller.Index() as ViewResult;

            // Now lets evrify whether the result contains our book entries or not
            var model = (List<Book>)result.ViewData.Model;

            CollectionAssert.Contains(model, book1);
            CollectionAssert.Contains(model, book2);
            CollectionAssert.Contains(model, book3);
            CollectionAssert.Contains(model, book4);

            // Uncomment the below line and the test will start failing
            //CollectionAssert.Contains(model, book5);
        }
        [Test]
        public void Details()
        {
            ViewResult result = controller.Details(2) as ViewResult;

            Assert.AreEqual(result.Model, book1);
        }
        [Test]
        public void Create()
        {
            // Lets create a new book object
            Book newBook = new Book { Id = 7, BookName = "new", Authorname = "new"};

            //Add the book object in the book object;
            controller.Create(newBook);

            List<Book> bookList = _booksRepo.GetAllBooks();
            CollectionAssert.Contains(bookList, newBook);
        }
        [Test]
        public void Edit()
        {
            // Lets create a valid book objct to add into
            Book editedBook = new Book { Id = 1, BookName = "Book1", Authorname = "John" };

            //Lets create a edit using controller
            controller.Edit(editedBook);

            List<Book> bookList = _booksRepo.GetAllBooks();
            CollectionAssert.Contains(bookList, editedBook);
        }

        [Test]
        public void Divide_Validvalue_ValidOutput()
        {
            // Arrange
            int testA = 4;
            int testB = 2;
           
            decimal expectedvalue = 2;

            //Create a mock object of a mail controller 
            var mockMailClient = new Moq.Mock<BooksController>();

            //Configure dummy method so that it return true when it gets any string as parameters to the method
            mockMailClient.Setup(client => client.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            // Act
            var result = controller.Divide(testA, testB);

            // Assert 
            Assert.AreEqual(expectedvalue, result, "The value of action divide was not as expected");
        }

    }
}
