using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Borrowings
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Borrowing> BorrowingList { get; set; } = new List<Borrowing>();

        public async Task OnGetAsync()
        {
            BorrowingList = await _context.Borrowing
                .Include(b => b.Member)
                .Include(b => b.Book)
                .ToListAsync();
        }
    }
}
