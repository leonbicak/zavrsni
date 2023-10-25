using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace izlazniracuni.Models
{
    public class izlazni_racun
    {
        [Key]
        public int ID_izlazni_racun { get; set; }


        [ForeignKey("ID_ugovor")]
        public ugovor? ugovor { get; set; }

        public DateTime datum_usluge { get; set; }

        public DateTime datum_dospjeca { get; set; }

        public int cijena { get; set; }

        public string? broj_racuna { get; set; }
    }
}
