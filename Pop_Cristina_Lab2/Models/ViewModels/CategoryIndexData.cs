using System.Collections.Generic;

namespace Pop_Cristina_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Models.Category> Categories { get; set; } = new List<Models.Category>();
        public IEnumerable<Models.Book> Books { get; set; } = new List<Models.Book>();
    }
}
