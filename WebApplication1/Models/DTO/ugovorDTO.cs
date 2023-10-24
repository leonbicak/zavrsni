namespace izlazniracuni.Models.DTO
{
    public class ugovorDTO
    {
        public int ID_ugovor { get; set; }
        public int ID_kupac2 { get; set; }

        public DateTime datum_pocetka { get; set; }
        public DateTime datum_zavrsetka { get; set; }
        public string urudzbeni_broj { get; set; }
        public string kupac2 { get; set; }
    }
}
