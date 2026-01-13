using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Comenzi
{
    public class CreateModel : PageModel
    {
        private readonly restaurant_mediiContext _context;

        public CreateModel(restaurant_mediiContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comanda Comanda { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedProduseIDs { get; set; } = new();

        [BindProperty]
        public int CantitateImplicita { get; set; } = 1;

        public List<SelectListItem> ProduseItems { get; set; } = new();

        public IActionResult OnGet()
        {
            ViewData["ClientID"] = new SelectList(
                _context.Client.Select(c => new { c.ID, Name = c.Nume + " " + c.Prenume }),
                "ID", "Name");

            ProduseItems = _context.Produs
                .Select(p => new SelectListItem
                {
                    Value = p.ID.ToString(),
                    Text = p.Nume + " (" + p.Pret + " lei)"
                })
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Comanda.Add(Comanda);
            await _context.SaveChangesAsync();

            foreach (var produsId in SelectedProduseIDs)
            {
                _context.ComandaProdus.Add(new ComandaProdus
                {
                    ComandaID = Comanda.ID,
                    ProdusID = produsId,
                    Cantitate = CantitateImplicita
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
