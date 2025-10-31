using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using Pop_Cristina_Lab2.Models.ViewModels;

namespace Pop_Cristina_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        // păstrăm lista simplă, ca la scaffolding
        public IList<Category> Category { get; set; } = default!;

        // lab 4
        public CategoryIndexData CategoryData { get; set; } = new();
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            // luăm toate categoriile cu cărțile lor
            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            Category = CategoryData.Categories.ToList();

            if (id != null)
            {
                CategoryID = id.Value;
                var selectedCategory = CategoryData.Categories
                    .Single(c => c.ID == id.Value);

                // din fiecare legătură BookCategory luăm Book-ul
                CategoryData.Books = selectedCategory.BookCategories?
                    .Select(bc => bc.Book)
                    .Where(b => b != null)!
                    .ToList()
                    ?? new List<Book>();
            }
        }
    }
}
