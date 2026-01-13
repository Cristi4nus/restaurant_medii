using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant_medii.Models
{
    public class Comanda
    {
        public int ID { get; set; }

        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataComenzii { get; set; }

        public ICollection<ComandaProdus> ProduseComanda { get; set; } = new List<ComandaProdus>();

        [NotMapped]
        public int Total => ProduseComanda?.Sum(cp => cp.Cantitate * cp.Produs.Pret) ?? 0;
    }
}
