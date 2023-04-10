namespace FPTBook.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string FullName { get; set; }

        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}