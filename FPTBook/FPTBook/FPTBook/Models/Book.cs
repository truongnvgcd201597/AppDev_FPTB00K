namespace FPTBook.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string BookName { get; set; }
        public decimal BookPrice { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int BookCount { get; set; }
        public int PublisherID { get; set; }

        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}