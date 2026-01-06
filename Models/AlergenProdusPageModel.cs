using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurant_medii.Data;
using restaurant_medii.Models;
using System.Collections.Generic;
using System.Linq;

public class AlergenProdusPageModel : PageModel
{
    public List<AlergenAtribuit> AlergeniAtribuitiList;

    public void PopulateAssignedAlergenData(restaurant_mediiContext context, Produs produs)
    {
        var totiAlergenii = context.Alergen;
        var alergeniProdus = new HashSet<int>(
            produs.AlergeniProduse.Select(ap => ap.AlergenID));

        AlergeniAtribuitiList = new List<AlergenAtribuit>();

        foreach (var alergen in totiAlergenii)
        {
            AlergeniAtribuitiList.Add(new AlergenAtribuit
            {
                AlergenID = alergen.ID,
                Nume = alergen.NumeAlergen,
                Atribuit = alergeniProdus.Contains(alergen.ID)
            });
        }
    }

    public void UpdateProdusAlergeni(
        restaurant_mediiContext context,
        string[] selectedAlergeni,
        Produs produsToUpdate)
    {
        if (selectedAlergeni == null)
        {
            produsToUpdate.AlergeniProduse = new List<AlergenProdus>();
            return;
        }

        var selectedHS = new HashSet<string>(selectedAlergeni);
        var alergeniExistenti = new HashSet<int>(
            produsToUpdate.AlergeniProduse.Select(ap => ap.AlergenID));

        foreach (var alergen in context.Alergen)
        {
            if (selectedHS.Contains(alergen.ID.ToString()))
            {
                if (!alergeniExistenti.Contains(alergen.ID))
                {
                    produsToUpdate.AlergeniProduse.Add(new AlergenProdus
                    {
                        ProdusID = produsToUpdate.ID,
                        AlergenID = alergen.ID
                    });
                }
            }
            else
            {
                if (alergeniExistenti.Contains(alergen.ID))
                {
                    var deSters = produsToUpdate.AlergeniProduse
                        .SingleOrDefault(ap => ap.AlergenID == alergen.ID);
                    context.Remove(deSters);
                }
            }
        }
    }
}
