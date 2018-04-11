using System;

namespace TddMvcNunit.Models
{
    public class UnitOfWork : IDisposable
    {
        private TestEntities entities = null;
        public UnitOfWork()
        {
            entities = new TestEntities();
            BookRepository = new BooksRepository(entities);
        }

        public UnitOfWork(IBooksRepository booksRepo)
        {
            BookRepository = booksRepo;
        }

        public IBooksRepository BookRepository
        {
            get;
            private set;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                entities = null;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}