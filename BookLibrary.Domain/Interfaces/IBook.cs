using BookLibrary.Domain.Dtos;
using BookLibrary.Domain.Entities;
using BookLibrary.Domain.Enums;

namespace BookLibrary.Domain.Interfaces
{
    public interface IBook
    {
        OpStatus Add(Book book);
        OpStatus Update(Book book);
        OpStatus Delete(Guid id);
        Book GetById(Guid id);
        List<BookListResponse> GetAll(); 
    }
}
