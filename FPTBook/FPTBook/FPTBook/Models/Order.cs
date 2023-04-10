namespace FPTBook.Models
{

    public class Order
    {
        public int OrderID { get; set; }
        public decimal Payment { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}