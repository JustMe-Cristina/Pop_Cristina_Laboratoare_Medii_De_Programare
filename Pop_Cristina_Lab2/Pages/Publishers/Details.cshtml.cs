using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using System.Threading.Tasks;

namespace Pop_Cristina_Lab2.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public DetailsModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Publisher = await _context.Publisher
                .Include(p => p.Books)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Publisher == null)
                return NotFound();

            return Page();
        }
    }
}
