using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public CreateModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name");
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name");
                ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "Name");
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
