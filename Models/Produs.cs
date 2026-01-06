namespace restaurant_medii.Models
{
    public class Produs
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public int Pret { get; set; }

        public int CategorieID { get; set; }
        public Categorie? Categorie { get; set; }
    }
}
