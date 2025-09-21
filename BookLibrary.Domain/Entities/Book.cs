using BookLibrary.Domain.Enums;

namespace BookLibrary.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; } 
        public int PageCount { get; set; } 
        public string Description { get; set; }


        public Lanagage Lanagage { get; set; } = Lanagage.EN;
        public Publisher Publisher { get; set; }
    }
}
