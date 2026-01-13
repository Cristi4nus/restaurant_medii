using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Comenzi
{
    public class IndexModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public IndexModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        public IList<Comanda> Comanda { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Comanda = await _context.Comanda
                .Include(c => c.Client)
                .Include(c => c.ProduseComanda)
                    .ThenInclude(cp => cp.Produs)
                .ToListAsync();
        }
    }
}
