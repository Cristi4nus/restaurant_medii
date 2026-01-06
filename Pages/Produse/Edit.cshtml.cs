using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Produse
{
    public class EditModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public EditModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produs Produs { get; set; } = default!;

        // Dropdown pentru categorii
        public SelectList CategoriiSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            // Include pentru a încărca categoria produsului
            Produs = await _context.Produs
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Produs == null)
                return NotFound();

            // Populează dropdown-ul și preselectează categoria curentă
            CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume", Produs.CategorieID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reîncarcă dropdown-ul dacă există erori
                CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume", Produs.CategorieID);
                return Page();
            }

            _context.Attach(Produs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdusExists(Produs.ID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool ProdusExists(int id)
        {
            return _context.Produs.Any(e => e.ID == id);
        }
    }
}
