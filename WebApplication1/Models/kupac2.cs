using System.ComponentModel.DataAnnotations;

namespace izlazniracuni.Models
{
    public class kupac2
    {
        [Key]

        [Required]
        public int ID_kupac2 { get; set; }
        public string? ime { get; set; }

        public string? iban { get; set; }

        public string? adresa { get; set; }
    }
}
