using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pop_Cristina_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Display(Name = "Book Title")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Publishing Date")]
        public DateTime PublishingDate { get; set; }

        // Publisher
        [Display(Name = "Publisher")]
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }

        // Author (relație 1-n)
        [Display(Name = "Author")]
        public int? AuthorID { get; set; }
        public Author? Author { get; set; }
    }
}
