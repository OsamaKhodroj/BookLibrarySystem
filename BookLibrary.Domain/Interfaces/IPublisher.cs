using BookLibrary.Domain.Entities;
using BookLibrary.Domain.Enums;

namespace BookLibrary.Domain.Interfaces
{
    public interface IPublisher
    {
        OpStatus Add(Publisher publisher);
        OpStatus Update(Publisher publisher);
        OpStatus Delete(Guid id);
        Publisher GetById(Guid id);
        List<Publisher> GetAll();
    }
}
