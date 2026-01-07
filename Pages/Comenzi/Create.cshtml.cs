using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using restaurant_medii.Data;
using restaurant_medii.Models;

namespace restaurant_medii.Pages.Comenzi
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
            var clientList = _context.Client
                .Select(c => new
                {
                    c.ID,
                    ClientFullName = c.Nume + " " + c.Prenume
                });
            var produsList = _context.Produs
                .Select(p => new
                {
                    p.ID,
                    ProdusFullName = p.Nume + " (" + p.Pret + " lei)"
                });

            ViewData["ClientID"] = new SelectList(clientList, "ID", "ClientFullName");
            ViewData["ProdusID"] = new SelectList(produsList, "ID", "ProdusFullName");

            return Page();
        }


        [BindProperty]
        public Comanda Comanda { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comanda.Add(Comanda);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
