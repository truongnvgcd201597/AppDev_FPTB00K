namespace FPTBook.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }
        public string PublisherAddress { get; set; }
        public string PublisherPhoneNumber { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}