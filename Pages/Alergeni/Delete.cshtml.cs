using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Alergeni
{
    public class DeleteModel : PageModel
    {
        private readonly restaurant_medii.Data.restaurant_mediiContext _context;

        public DeleteModel(restaurant_medii.Data.restaurant_mediiContext context)
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

            var alergen = await _context.Alergen.FirstOrDefaultAsync(m => m.ID == id);

            if (alergen == null)
            {
                return NotFound();
            }
            else
            {
                Alergen = alergen;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alergen = await _context.Alergen.FindAsync(id);
            if (alergen != null)
            {
                Alergen = alergen;
                _context.Alergen.Remove(Alergen);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
