using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pop_Cristina_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Book Title")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Publishing Date")]
        public DateTime PublishingDate { get; set; }

        public int? AuthorID { get; set; }
        public Author? Author { get; set; }

        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }

        // Lab 3 many-to-many
        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
