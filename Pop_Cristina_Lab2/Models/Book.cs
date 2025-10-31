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
        [Display(Name = "Book title")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Publishing date")]
        public DateTime PublishingDate { get; set; }

        // FK spre Author
        [Display(Name = "Author")]
        public int? AuthorID { get; set; }
        public Author? Author { get; set; }

        // FK spre Publisher
        [Display(Name = "Publisher")]
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }

        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
