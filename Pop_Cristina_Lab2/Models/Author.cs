using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pop_Cristina_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Display(Name = "Author Name")]
        public string Name { get; set; } = string.Empty;

        // un autor poate avea mai multe cărți
        public ICollection<Book>? Books { get; set; }
    }
}
