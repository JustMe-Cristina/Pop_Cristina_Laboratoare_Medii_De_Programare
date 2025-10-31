using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using System.Threading.Tasks;

namespace Pop_Cristina_Lab2.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public CreateModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
