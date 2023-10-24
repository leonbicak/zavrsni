using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace izlazniracuni.Models
{
    public class ugovor
    {
        [Key]
        public int ID_ugovor { get; set; }

        [ForeignKey("ID_kupac2")]
        public kupac2? kupac2 { get; set; }

        public DateTime? datum_pocetka { get; set; }

        public DateTime? datum_zavrsetka { get; set; }

        public string? urudzbeni_broj { get; set; }
    }
}
