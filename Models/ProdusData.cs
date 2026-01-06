namespace restaurant_medii.Models
{
    public class ProdusData
    {
        public IEnumerable<Produs> Produse { get; set; }
        public IEnumerable<Alergen> Alergeni { get; set; }
        public IEnumerable<AlergenProdus> AlergeniProduse { get; set; }
    }
}
