using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public SelectList Publishers { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? PublisherID { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            // baza query-ului
            var booksQuery = _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsQueryable();

            // căutare după titlu
            if (!string.IsNullOrEmpty(SearchString))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(SearchString));
            }

            // filtrare după publisher
            if (PublisherID.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.PublisherID == PublisherID.Value);
            }

            // sortare
            switch (SortOrder)
            {
                case "title_desc":
                    booksQuery = booksQuery.OrderByDescending(b => b.Title);
                    break;
                case "price_asc":
                    booksQuery = booksQuery.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    booksQuery = booksQuery.OrderByDescending(b => b.Price);
                    break;
                default:
                    booksQuery = booksQuery.OrderBy(b => b.Title);
                    break;
            }

            // pentru dropdown-ul de publisheri
            Publishers = new SelectList(await _context.Publisher.ToListAsync(), "ID", "Name");

            Book = await booksQuery.ToListAsync();
        }
    }
}
