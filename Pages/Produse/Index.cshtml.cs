using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Produse
{
    public class IndexModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public IndexModel(restaurant_mediiContext context)
        {
            _context = context;
        }
        public IList<Produs> Produs { get; set; } = default!;
        public ProdusData ProdusD { get; set; }
        public int ProdusID { get; set; }
        public int AlergenID { get; set; }

        public async Task OnGetAsync(int? id, int? alergenID)
        {
            ProdusD = new ProdusData();

            Produs = await _context.Produs
                .Include(p => p.Categorie)
                .Include(p => p.AlergeniProduse)
                    .ThenInclude(ap => ap.Alergen)
                .AsNoTracking()
                .OrderBy(p => p.Nume)
                .ToListAsync();

            ProdusD.Produse = Produs;

            if (id != null)
            {
                ProdusID = id.Value;

                var produs = Produs
                    .Where(p => p.ID == id.Value)
                    .Single();

                ProdusD.Alergeni = produs.AlergeniProduse
                    .Select(ap => ap.Alergen);
            }
        }
    }
}
