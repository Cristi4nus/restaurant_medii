using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using restaurant_medii.Data;
using restaurant_medii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_medii.Pages.Produse
{
    [Authorize(Roles = "ADMIN")]
    public class CreateModel : AlergenProdusPageModel
    {
        private readonly restaurant_mediiContext _context;

        public CreateModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        public SelectList CategoriiSelectList { get; set; }

        [BindProperty]
        public Produs Produs { get; set; } = default!;

        public IActionResult OnGet()
        {
            CategoriiSelectList = new SelectList(_context.Categorie, "ID", "Nume");

            var produs = new Produs();
            produs.AlergeniProduse = new List<AlergenProdus>();

            PopulateAssignedAlergenData(_context, produs);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedAlergeni)
        {
            var newProdus = new Produs();

            if (selectedAlergeni != null)
            {
                newProdus.AlergeniProduse = new List<AlergenProdus>();

                foreach (var alergen in selectedAlergeni)
                {
                    var alergenToAdd = new AlergenProdus
                    {
                        AlergenID = int.Parse(alergen)
                    };

                    newProdus.AlergeniProduse.Add(alergenToAdd);
                }
            }

            Produs.AlergeniProduse = newProdus.AlergeniProduse;

            _context.Produs.Add(Produs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
