using System.Collections.Generic;

namespace Pop_Cristina_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Models.Publisher> Publishers { get; set; } = new List<Models.Publisher>();
        public IEnumerable<Models.Book> Books { get; set; } = new List<Models.Book>();
    }
}
