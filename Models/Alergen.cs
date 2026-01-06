namespace restaurant_medii.Models
{
    public class Alergen
    {
        public int ID { get; set; }
        public string NumeAlergen { get; set; }
        public ICollection<AlergenProdus>? AlergeniProduse { get; set; }

    }
}
