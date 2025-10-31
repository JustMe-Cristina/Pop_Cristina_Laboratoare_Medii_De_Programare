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
    public class EditModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public EditModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.AuthorEntity)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name", Book.PublisherID);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "Name", Book.AuthorID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name", Book.PublisherID);
                ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "Name", Book.AuthorID);
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Book.Any(e => e.ID == Book.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
