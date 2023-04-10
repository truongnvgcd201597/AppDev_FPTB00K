namespace FPTBook.Models
{
    public class Author
    {

        public int AuthorID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}