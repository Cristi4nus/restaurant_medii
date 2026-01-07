using System.ComponentModel.DataAnnotations;

namespace restaurant_medii.Models
{
    public class Client
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage =
"Numele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau AnaMaria")]
        [StringLength(30, MinimumLength = 3)]

        public string? Nume { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage =
"Prenumele trebuie sa inceapa cu majuscula (ex. Ana sau Ana Maria sau AnaMaria")]
        [StringLength(30, MinimumLength = 3)]

        public string? Prenume { get; set; }
        public string Email { get; set; }
        [RegularExpression(@"^(0[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{3}|0[0-9]{9})$", ErrorMessage = "Numarul de telefon trebuie sa fie de forma '0722-123-123', '0722.123.123', '0722 123 123' sau '0722123123' si sa inceapa cu 0")]
        public string Telefon { get; set; }

        [Display(Name = "Nume complet")]
        public string NumeComplet
        {
            get
            {
                return Nume + " " + Prenume;
            }
        }
    }
}
