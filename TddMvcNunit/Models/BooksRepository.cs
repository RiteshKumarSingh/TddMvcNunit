using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace TddMvcNunit.Models
{
    public class BooksRepository: IBooksRepository
    {
        TestEntities entities = null;

        public BooksRepository(TestEntities entities)
        {
            this.entities = entities;
        }

        public List<Book> GetAllBooks()
        {
            return entities.Books.ToList();
        }
        public Book GetBookById(int id)
        {
            return entities.Books.SingleOrDefault(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            entities.Books.Add(book);
            Save();
        }

        public void UpdateBook(Book book)
        {

            entities.Entry(book).State = EntityState.Modified;
            Save();
        }

        public void DeleteBook(Book book)
        {
            entities.Books.Remove(book);
            Save();
        }

        public void Save()
        {
            entities.SaveChanges();
        }
    }
}