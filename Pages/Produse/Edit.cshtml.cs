using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_medii.Pages.Produse
{
    [Authorize(Roles = "ADMIN")]
    public class EditModel : AlergenProdusPageModel
    {
        private readonly restaurant_mediiContext _context;

        public EditModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produs Produs { get; set; } = default!;

        public SelectList CategoriiSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Produs = await _context.Produs
                .Include(p => p.Categorie)
                .Include(p => p.AlergeniProduse)
                    .ThenInclude(ap => ap.Alergen)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Produs == null)
                return NotFound();

            CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume", Produs.CategorieID);

            PopulateAssignedAlergenData(_context, Produs);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedAlergeni)
        {
            if (id == null)
                return NotFound();

            var produsToUpdate = await _context.Produs
                .Include(p => p.Categorie)
                .Include(p => p.AlergeniProduse)
                    .ThenInclude(ap => ap.Alergen)
                .FirstOrDefaultAsync(p => p.ID == id);

            if (produsToUpdate == null)
                return NotFound();

            if (await TryUpdateModelAsync<Produs>(
                produsToUpdate,
                "Produs",
                p => p.Nume,
                p => p.Pret,
                p => p.CategorieID))
            {
                UpdateProdusAlergeni(_context, selectedAlergeni, produsToUpdate);

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateProdusAlergeni(_context, selectedAlergeni, produsToUpdate);
            PopulateAssignedAlergenData(_context, produsToUpdate);
            CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume", produsToUpdate.CategorieID);

            return Page();
        }
    }
}
