using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using Pop_Cristina_Lab2.Models.ViewModels;
using System.Collections.Generic;

namespace Pop_Cristina_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        // ce cere .cshtml
        public CategoryIndexData CategoryData { get; set; } = new CategoryIndexData();
        public int? CategoryID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            CategoryData = new CategoryIndexData
            {
                Categories = await _context.Category
                    .Include(c => c.BookCategories!)
                        .ThenInclude(bc => bc.Book)
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync()
            };

            if (id != null)
            {
                CategoryID = id;
                var category = CategoryData.Categories!.FirstOrDefault(c => c.ID == id);
                if (category != null)
                {
                    CategoryData.Books = category.BookCategories!
                        .Select(bc => bc.Book!)
                        .ToList();
                }
            }
        }
    }
}
