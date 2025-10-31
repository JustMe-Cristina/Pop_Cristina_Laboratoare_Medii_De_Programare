using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pop_Cristina_Lab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Member name")]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
