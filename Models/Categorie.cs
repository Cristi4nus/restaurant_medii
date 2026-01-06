namespace restaurant_medii.Models
{
    public class Categorie
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public ICollection<Produs>? Produse { get; set; }
    }
}
