using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Produse
{
    public class CreateModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public CreateModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        // Dropdown pentru categorii
        public SelectList CategoriiSelectList { get; set; }

        [BindProperty]
        public Produs Produs { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Încarcă lista de categorii pentru dropdown
            CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Dacă există erori, reîncarcă dropdown-ul
                CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume");
                return Page();
            }

            _context.Produs.Add(Produs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
