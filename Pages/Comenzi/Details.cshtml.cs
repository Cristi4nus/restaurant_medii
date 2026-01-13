using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Comenzi
{
    public class DetailsModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public DetailsModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        public Comanda Comanda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Comanda = await _context.Comanda
                .Include(c => c.Client)
                .Include(c => c.ProduseComanda)
                    .ThenInclude(cp => cp.Produs)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (Comanda == null)
                return NotFound();

            return Page();
        }
    }
}
