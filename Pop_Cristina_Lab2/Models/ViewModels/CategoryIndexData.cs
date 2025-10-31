using System.Collections.Generic;

namespace Pop_Cristina_Lab2.Models.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category>? Categories { get; set; }
        public IEnumerable<Book>? Books { get; set; }
    }
}
