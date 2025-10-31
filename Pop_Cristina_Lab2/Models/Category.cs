using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pop_Cristina_Lab2.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
