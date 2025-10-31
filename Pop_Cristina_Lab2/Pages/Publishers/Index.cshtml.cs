using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using Pop_Cristina_Lab2.Models.ViewModels;

namespace Pop_Cristina_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; } = default!;

        public PublisherIndexData PublisherData { get; set; } = new();
        public int PublisherID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            // AICI ÎNCĂRCĂM EDITURILE
            var publishers = await _context.Publisher
                .Include(p => p.Books)
                    .ThenInclude(b => b.Author)
                .OrderBy(p => p.Name)
                .ToListAsync();

            PublisherData.Publishers = publishers;
            Publisher = publishers; // ca să nu mai dea null în view

            if (id != null)
            {
                PublisherID = id.Value;
                var selected = publishers.Single(p => p.ID == id.Value);
                PublisherData.Books = selected.Books ?? new List<Book>();
            }
        }
    }
}
