using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pop_Cristina_Lab2.Data;
using Pop_Cristina_Lab2.Models;
using Pop_Cristina_Lab2.Models.ViewModels;

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

        // ASTA îți lipsea
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories!)
                    .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null) return NotFound();

            Book = book;

            // dropdown-uri
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName", Book.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name", Book.PublisherID);

            // checkbox-urile pt categorii
            PopulateAssignedCategoryData(book);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            if (Book == null) return NotFound();

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories!)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == Book.ID);

            if (bookToUpdate == null) return NotFound();

            // actualizăm câmpurile de bază
            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                b => b.Title, b => b.Price, b => b.PublishingDate,
                b => b.AuthorID, b => b.PublisherID))
            {
                // actualizăm categoriile
                UpdateBookCategories(selectedCategories, bookToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // dacă ajungem aici, a fost invalid -> refacem listele
            PopulateAssignedCategoryData(bookToUpdate);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName", bookToUpdate.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "Name", bookToUpdate.PublisherID);
            return Page();
        }

        private void PopulateAssignedCategoryData(Book book)
        {
            var allCategories = _context.Category;
            var bookCategories = new HashSet<int>(book.BookCategories!.Select(c => c.CategoryID));

            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }
        }

        private void UpdateBookCategories(string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null || selectedCategories.Length == 0)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedHS = new HashSet<string>(selectedCategories);
            var currentCategories = new HashSet<int>(
                bookToUpdate.BookCategories!.Select(c => c.CategoryID));

            foreach (var cat in _context.Category)
            {
                if (selectedHS.Contains(cat.ID.ToString()))
                {
                    if (!currentCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories!.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (currentCategories.Contains(cat.ID))
                    {
                        var toRemove = bookToUpdate.BookCategories!
                            .FirstOrDefault(i => i.CategoryID == cat.ID);
                        if (toRemove != null)
                            _context.Remove(toRemove);
                    }
                }
            }
        }
    }
}
