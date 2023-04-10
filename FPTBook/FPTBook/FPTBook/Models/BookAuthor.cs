﻿
namespace FPTBook.Models
{
    public class BookAuthor
    {

        public int BookID { get; set; }
        public int AuthorID { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}