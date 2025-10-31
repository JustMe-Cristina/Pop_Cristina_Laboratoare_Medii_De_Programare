using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using System.Collections.Generic;

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

        // pentru checkbox-uri
        public List<AssignedCategoryData> AssignedCategories { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
                return NotFound();

            PopulateAssignedCategoryData(Book);

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name", Book.PublisherID);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "Name", Book.AuthorID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
                return NotFound();

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (bookToUpdate == null)
                return NotFound();

            // actualizăm câmpurile simple
            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                b => b.Title, b => b.Price, b => b.PublishingDate, b => b.PublisherID, b => b.AuthorID))
            {
                UpdateBookCategories(selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // dacă nu a mers model state, refacem listele
            PopulateAssignedCategoryData(bookToUpdate);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name", bookToUpdate.PublisherID);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "Name", bookToUpdate.AuthorID);
            return Page();
        }

        private void PopulateAssignedCategoryData(Book book)
        {
            var allCategories = _context.Category;
            var bookCategories = new HashSet<int>(book.BookCategories?.Select(c => c.CategoryID) ?? Enumerable.Empty<int>());

            AssignedCategories = new List<AssignedCategoryData>();

            foreach (var category in allCategories)
            {
                AssignedCategories.Add(new AssignedCategoryData
                {
                    CategoryID = category.ID,
                    CategoryName = category.CategoryName,
                    Assigned = bookCategories.Contains(category.ID)
                });
            }
        }

        private void UpdateBookCategories(string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                // dacă nu e bifat nimic, golește lista
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedHS = new HashSet<string>(selectedCategories);
            var currentCategories = new HashSet<int>(
                bookToUpdate.BookCategories?.Select(bc => bc.CategoryID) ?? Enumerable.Empty<int>());

            foreach (var category in _context.Category)
            {
                // dacă e bifat ACUM și nu era înainte → adăugăm
                if (selectedHS.Contains(category.ID.ToString()))
                {
                    if (!currentCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories!.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = category.ID
                        });
                    }
                }
                else
                {
                    // dacă NU e bifat ACUM dar era înainte → ștergem
                    if (currentCategories.Contains(category.ID))
                    {
                        var toRemove = bookToUpdate.BookCategories!
                            .FirstOrDefault(bc => bc.CategoryID == category.ID);
                        if (toRemove != null)
                        {
                            _context.BookCategory.Remove(toRemove);
                        }
                    }
                }
            }
        }
    }
}
