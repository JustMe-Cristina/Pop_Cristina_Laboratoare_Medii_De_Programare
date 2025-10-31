using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> PublisherList { get; set; } = new List<Publisher>();

        public async Task OnGetAsync()
        {
            PublisherList = await _context.Publisher
                .Include(p => p.Books!)              // <- ! aici
                    .ThenInclude(b => b.Author)
                .ToListAsync();
        }
    }
}
