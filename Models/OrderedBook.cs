using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FPTBook.Models
{
    public class OrderedBook
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [DisplayName("User Id:")]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [DisplayName("Quantity: ")]
        [Required]
        public int Quantity { get; set; }

        [DisplayName("Is Ordered: ")]
        [Required]
        public bool IsOrdered { get; set; }
        public ICollection<OrderOrderedBook> OrderOrderedBooks { get; set; }
    }
}
