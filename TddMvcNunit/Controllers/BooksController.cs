using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TddMvcNunit.Models;

namespace TddMvcNunit.Controllers
{
    public class BooksController : Controller
    {
        private UnitOfWork unitOfWork = null;

        public BooksController() : this(new UnitOfWork())
        { }
        public BooksController(UnitOfWork uow)
        {
            this.unitOfWork = uow;
        }

        public ActionResult Index()
        {
            List<Book> books = unitOfWork.BookRepository.GetAllBooks();
            return View(books);
        }

        public ActionResult Details(int id)
        {
            Book book = unitOfWork.BookRepository.GetBookById(id);

            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookRepository.AddBook(book);
                unitOfWork.BookRepository.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            Book book = unitOfWork.BookRepository.GetBookById(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookRepository.UpdateBook(book);
                unitOfWork.BookRepository.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            Book book = unitOfWork.BookRepository.GetBookById(id);
            unitOfWork.BookRepository.DeleteBook(book);
            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            Book book = unitOfWork.BookRepository.GetBookById(id);
            unitOfWork.BookRepository.DeleteBook(book);
            unitOfWork.BookRepository.Save();
            return View("Deleted");
        }

        public ActionResult Deleted()
        {
            return View();
        }

        public decimal Divide(int a, int b)
        {
            string from = string.Empty;
            string to= string.Empty;
            string subject= string.Empty;
            string body= string.Empty;

            if (b == 0)
            {
                throw new DivideByZeroException("Lets ensure.");
            }
            SendEmail(from, to, subject, body);
            return (a / b);

        }

        public bool SendEmail(string from, string to, string subject, string body)
        {
            System.Threading.Thread.Sleep(5000);
            return true;
        }
    }
}