namespace restaurant_medii.Models
{
    public class ComandaProdus
    {
        public int ID { get; set; }

        public int ComandaID { get; set; }
        public Comanda Comanda { get; set; }

        public int ProdusID { get; set; }
        public Produs Produs { get; set; }

        public int Cantitate { get; set; }
    }
}
