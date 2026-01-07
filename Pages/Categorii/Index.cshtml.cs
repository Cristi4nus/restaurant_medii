using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Categorii
{
    public class IndexModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public IndexModel(restaurant_mediiContext context)
        {
            _context = context;
        }
        public IList<Categorie> Categorie { get; set; } = default!;
        public CategorieIndexData CategorieData { get; set; }

        public int CategorieID { get; set; }
        public int ProdusID { get; set; }

        public async Task OnGetAsync(int? id, int? produsID)
        {
            CategorieData = new CategorieIndexData();

            CategorieData.Categorii = await _context.Categorie
                .Include(c => c.Produse)
                .OrderBy(c => c.Nume)
                .ToListAsync();
            Categorie = CategorieData.Categorii.ToList();

            if (id != null)
            {
                CategorieID = id.Value;

                var categorie = CategorieData.Categorii
                    .Where(c => c.ID == id.Value)
                    .Single();

                CategorieData.Produse = categorie.Produse;
            }
        }
    }
}
