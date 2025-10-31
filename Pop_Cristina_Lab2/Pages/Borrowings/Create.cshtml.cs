using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Borrowings
{
    public class CreateModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public CreateModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = new();

        public IActionResult OnGet()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "ID", "Title");
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            Borrowing.BorrowDate = System.DateTime.Today;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["BookId"] = new SelectList(_context.Book, "ID", "Title");
                ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
                return Page();
            }

            _context.Borrowing.Add(Borrowing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
