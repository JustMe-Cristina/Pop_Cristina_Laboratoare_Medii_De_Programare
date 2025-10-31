using System.Collections.Generic;

namespace Pop_Cristina_Lab2.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        // relația inversă: o categorie poate fi pe mai multe cărți
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
