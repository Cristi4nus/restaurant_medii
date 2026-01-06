namespace restaurant_medii.Models
{
    public class AlergenProdus
    {
        public int ID { get; set; }

        public int ProdusID { get; set; }
        public Produs Produs { get; set; }

        public int AlergenID { get; set; }
        public Alergen Alergen { get; set; }
    }

}
