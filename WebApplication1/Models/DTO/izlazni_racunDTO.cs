namespace izlazniracuni.Models.DTO
{
    public class izlazni_racunDTO
    {
        public int ID_ugovor { get; set; }
        public int ID_izlazni_racun { get; set; }


        public DateTime? datum_usluge { get; set; }
        public DateTime? datum_dospjeca { get; set; }

        public int cijena { get; set; }

        public string? broj_racuna { get; set; }

        public string? ugovor { get; set; }


    }
}
