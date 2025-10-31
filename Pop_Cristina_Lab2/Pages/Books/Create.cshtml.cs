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

namespace Pop_Cristina_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Pop_Cristina_Lab2Context _context;

        public CreateModel(Pop_Cristina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // pentru checkboxes din Lab 3
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new();

        public IActionResult OnGet()
        {
            // dropdown Author
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");
            // dropdown Publisher
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            // checkboxes categorii (Lab 3)
            PopulateAssignedCategoryData();

            return Page();
        }

        private void PopulateAssignedCategoryData()
        {
            var allCategories = _context.Category;
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = false
                });
            }
        }

        public async Task<IActionResult> OnPostAsync(string[]? selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");
                ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
                PopulateAssignedCategoryData();
                return Page();
            }

            var newBook = Book;

            // legăm categoriile (Lab 3)
            if (selectedCategories != null && selectedCategories.Length > 0)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    newBook.BookCategories.Add(new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    });
                }
            }

            _context.Book.Add(newBook);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }

    // helper din Lab 3
    public class AssignedCategoryData
    {
        public int CategoryID { get; set; }
        public string? Name { get; set; }
        public bool Assigned { get; set; }
    }
}
