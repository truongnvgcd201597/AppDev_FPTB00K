namespace FPTBook.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}