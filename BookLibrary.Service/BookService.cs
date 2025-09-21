using BookLibrary.Domain.Dtos;
using BookLibrary.Domain.Entities;
using BookLibrary.Domain.Enums;
using BookLibrary.Domain.Interfaces;
using System.Collections.Specialized;
using System.Net.Mail;

namespace BookLibrary.Service
{
    public class BookService : IBook
    {
        private static List<Book> _books;

        public BookService()
        {
            if (_books == null)
            {
                _books = new List<Book>();
            }
        }


        /// <summary>
        /// This method adds a new book to the collection after validating its details.
        /// </summary>
        /// <param name="book">set book object</param>
        /// <returns></returns>
        public OpStatus Add(Book book)
        {
            try
            {
                CheckBookDetails(book);

                var isBookExists = _books.Any(b => b.ISBN == book.ISBN);
                if (isBookExists)
                    return OpStatus.AlreadyExists;

                _books.Add(book);
                return OpStatus.Success;
            }
            catch (Exception)
            {
                return OpStatus.Failure;
            }
        }

        /// <summary>
        /// this method deletes a book from the collection based on its ID.
        /// </summary>
        /// <param name="id">set id - Guid</param>
        /// <returns></returns>
        public OpStatus Delete(Guid id)
        {
            try
            {
                var book = _books.Where(q => q.Id == id).FirstOrDefault();
                if (book == null)
                    return OpStatus.NotFound;

                book.IsDeleted = true;
                //_books.Remove(book); 
                return OpStatus.Success;
            }
            catch (Exception)
            {
                return OpStatus.Failure;
            }
        }

        /// <summary>
        /// this method retrieves all books from the collection that are not marked as deleted.     
        /// </summary>
        /// <returns></returns>
        public List<BookListResponse> GetAll()
        {
            return _books
                .Select(b => new BookListResponse()
                {
                    Id = b.Id,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    PageCount = b.PageCount,
                    PublishedDate = b.PublishedDate,
                    Title = b.Title,
                    IsDeleted = b.IsDeleted
                })
                .ToList();
        }

        /// <summary>
        /// this method retrieves a book from the collection based on its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book GetById(Guid id)
        {
            var book = _books.Where(q => q.Id == id && !q.IsDeleted).FirstOrDefault();
            if (book == null)
            {
                return new();
            }
            return book;
        }

        /// <summary>
        /// this method updates the details of an existing book in the collection.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public OpStatus Update(Book book)
        {
            try
            {
                var existBook = _books.Where(q => q.Id == book.Id && !q.IsDeleted).FirstOrDefault();
                if (existBook == null)
                    return OpStatus.NotFound;


                existBook.ISBN = book.ISBN;
                existBook.Title = book.Title;
                existBook.Author = book.Author;
                existBook.PublishedDate = book.PublishedDate;
                existBook.PageCount = book.PageCount;
                existBook.Publisher = book.Publisher;
                existBook.Lanagage = book.Lanagage;
                existBook.Description = book.Description;

                return OpStatus.Success;

            }
            catch (Exception)
            {

                throw;
            }
        }



        #region Private Methods

        private bool CheckBookDetails(Book book)
        {


            if (string.IsNullOrEmpty(book.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            if (string.IsNullOrEmpty(book.Author))
                throw new ArgumentException("Author cannot be null or empty.");

            if (string.IsNullOrEmpty(book.ISBN))
                throw new ArgumentException("Invalid ISBN format.");

            if (book.PublishedDate > DateTime.Now)
                throw new ArgumentException("Published date cannot be in the future.");

            if (book.PageCount <= 0)
                throw new ArgumentException("Page count must be a positive integer.");

            //if (book.Publisher == null || string.IsNullOrEmpty(book.Publisher.Name))
            //    throw new ArgumentException("Publisher information is required.");


            return true;
        }

        #endregion
    }
}
