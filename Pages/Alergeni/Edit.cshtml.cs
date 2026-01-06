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

namespace restaurant_medii.Pages.Alergeni
{
    public class EditModel : PageModel
    {
        private readonly restaurant_medii.Data.restaurant_mediiContext _context;

        public EditModel(restaurant_medii.Data.restaurant_mediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Alergen Alergen { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergen =  await _context.Alergen.FirstOrDefaultAsync(m => m.ID == id);
            if (alergen == null)
            {
                return NotFound();
            }
            Alergen = alergen;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Alergen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlergenExists(Alergen.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AlergenExists(int id)
        {
            return _context.Alergen.Any(e => e.ID == id);
        }
    }
}
