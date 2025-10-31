namespace Pop_Cristina_Lab2.Models
{
    public class BookCategory
    {
        public int ID { get; set; }

        // FK spre Book
        public int BookID { get; set; }
        public Book Book { get; set; } = null!;

        // FK spre Category
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;
    }
}
