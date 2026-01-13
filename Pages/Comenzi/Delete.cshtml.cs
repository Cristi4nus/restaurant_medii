using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Comenzi
{
    public class DeleteModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public DeleteModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comanda Comanda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Comanda = await _context.Comanda
                .Include(c => c.Client)
                .Include(c => c.ProduseComanda)
                    .ThenInclude(cp => cp.Produs)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Comanda == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var comanda = await _context.Comanda
                .Include(c => c.ProduseComanda)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (comanda != null)
            {
                _context.ComandaProdus.RemoveRange(comanda.ProduseComanda);
                _context.Comanda.Remove(comanda);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
