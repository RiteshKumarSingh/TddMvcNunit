using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddMvcNunit.Models;

namespace TddMvcNunit.test.Repository
{
    class DummyBooksRepository: IBooksRepository
    {
        List<Book> m_books = null;
        public DummyBooksRepository(List<Book> books)
        {
            m_books = books;
        }
        public List<Book> GetAllBooks()
        {
            return m_books;
        }

        public Book GetBookById(int id)
        {
            return m_books.SingleOrDefault(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            m_books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            int id = book.Id;
            Book bookToUpdate = m_books.SingleOrDefault(b => b.Id == id);
            DeleteBook(bookToUpdate);
            m_books.Add(book);
        }

        public void DeleteBook(Book book)
        {
            m_books.Remove(book);
        }

        public void Save()
        {
            // Nothing to do here
        }
    }
}

