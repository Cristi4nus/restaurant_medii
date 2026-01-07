using System.ComponentModel.DataAnnotations;

namespace restaurant_medii.Models
{
    public class Comanda
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        public int? ProdusID { get; set; }
        public Produs? Produs { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataComenzii { get; set; }
    }
}
