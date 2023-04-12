using System.ComponentModel;

namespace FPTBook.Models;

public class Category
{
    public int Id { get; set; }
    [DisplayName("Category")]
    public string Name { get; set; }
}