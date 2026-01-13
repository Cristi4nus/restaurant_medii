using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant_medii.Models
{
    public class Produs
    {
        public int ID { get; set; }
        public string Nume { get; set; }

        [Range(0.01, 500)]
        public int Pret { get; set; }

        public int CategorieID { get; set; }
        public Categorie? Categorie { get; set; }
        public ICollection<Comanda>? Comenzi { get; set; }

        public ICollection<AlergenProdus>? AlergeniProduse { get; set; }
    }
}
