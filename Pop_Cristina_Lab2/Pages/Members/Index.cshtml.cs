using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;

namespace Pop_Cristina_Lab2.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public IndexModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Member> MemberList { get; set; } = new List<Member>();

        public async Task OnGetAsync()
        {
            MemberList = await _context.Member
                .OrderBy(m => m.FullName)
                .ToListAsync();
        }
    }
}
