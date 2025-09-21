namespace BookLibrary.Domain.Dtos
{
    public class BookListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsDeleted { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }
    }
}
