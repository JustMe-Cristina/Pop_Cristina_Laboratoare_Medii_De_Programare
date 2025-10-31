using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public EditModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var borrowing = await _context.Borrowing
                .Include(b => b.Member)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (borrowing == null) return NotFound();

            Borrowing = borrowing;

            ViewData["MemberID"] = new SelectList(
                await _context.Member.ToListAsync(),
                "ID",
                "FullName",
                Borrowing.MemberID
            );

            ViewData["BookID"] = new SelectList(
                await _context.Book.ToListAsync(),
                "ID",
                "Title",
                Borrowing.BookID
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // refacem listele dacă e invalid
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName", Borrowing.MemberID);
                ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", Borrowing.BookID);
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Borrowing.Any(e => e.ID == Borrowing.ID))
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
