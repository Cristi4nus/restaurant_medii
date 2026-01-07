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
        public string CategorieSortare { get; set; }
        public string AlergenSortare { get; set; }
        public string NumeCautare { get; set; }

        public async Task OnGetAsync(int? id, int? alergenID, string produsSortare, string stringCautare)
        {
            ProdusD = new ProdusData();

            CategorieSortare = String.IsNullOrEmpty(produsSortare) ? "categorie_desc" : "";
            AlergenSortare = produsSortare == "alergen" ? "alergen_desc" : "alergen";
            NumeCautare = stringCautare;

            Produs = await _context.Produs
                .Include(p => p.Categorie)
                .Include(p => p.AlergeniProduse)
                    .ThenInclude(ap => ap.Alergen)
                .AsNoTracking()
                .OrderBy(p => p.Nume)
                .ToListAsync();
            if (!String.IsNullOrEmpty(stringCautare))
            {
                ProdusD.Produse = Produs
                    .Where(s => s.Nume.Contains(stringCautare) || s.Categorie.Nume.Contains(stringCautare))
                    .ToList();
            }
            else
            {
                ProdusD.Produse = Produs;
            }


            if (id != null)
            {
                ProdusID = id.Value;

                var produs = Produs
                    .Where(p => p.ID == id.Value)
                    .Single();

                ProdusD.Alergeni = produs.AlergeniProduse
                    .Select(ap => ap.Alergen);
            }

            switch (produsSortare)
            {
                case "categorie_desc":
                    ProdusD.Produse = ProdusD.Produse
                        .OrderByDescending(p => p.Categorie.Nume)
                        .ToList();
                    break;

                case "alergen":
                    ProdusD.Produse = ProdusD.Produse
                        .OrderBy(p => p.AlergeniProduse
                            .Select(a => a.Alergen.NumeAlergen)
                            .FirstOrDefault())
                        .ToList();
                    break;

                case "alergen_desc":
                    ProdusD.Produse = ProdusD.Produse
                        .OrderByDescending(p => p.AlergeniProduse
                            .Select(a => a.Alergen.NumeAlergen)
                            .FirstOrDefault())
                        .ToList();
                    break;

                default:
                    ProdusD.Produse = ProdusD.Produse
                        .OrderBy(p => p.Nume)
                        .ToList();
                    break;
            }
        }

    }
}
