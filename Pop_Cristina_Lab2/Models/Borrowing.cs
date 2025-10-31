using System;
using System.ComponentModel.DataAnnotations;

namespace Pop_Cristina_Lab2.Models
{
    public class Borrowing
    {
        public int ID { get; set; }

        [Display(Name = "Member")]
        public int? MemberID { get; set; }
        public Member? Member { get; set; }

        [Display(Name = "Book")]
        public int? BookID { get; set; }
        public Book? Book { get; set; }

        [DataType(DataType.Date)]
        public DateTime BorrowDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }
    }
}
