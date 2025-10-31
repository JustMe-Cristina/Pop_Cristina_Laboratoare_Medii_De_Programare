using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public DetailsModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
            if (member == null) return NotFound();

            Member = member;
            return Page();
        }
    }
}
