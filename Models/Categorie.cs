using System.ComponentModel.DataAnnotations;

namespace restaurant_medii.Models
{
    public class Categorie
    {
        public int ID { get; set; }

        [Display(Name = "Categorie")]

        public string Nume { get; set; }
        public ICollection<Produs>? Produse { get; set; }
    }
}
