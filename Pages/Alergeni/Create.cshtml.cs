using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Alergeni
{
    public class CreateModel : PageModel
    {
        private readonly restaurant_medii.Data.restaurant_mediiContext _context;

        public CreateModel(restaurant_medii.Data.restaurant_mediiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Alergen Alergen { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Alergen.Add(Alergen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
