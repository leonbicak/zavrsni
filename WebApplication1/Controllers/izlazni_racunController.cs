using izlazniracuni.Data;
using izlazniracuni.Models;
using izlazniracuni.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Controllers;


namespace izlazniracuni.Controllers
{

    /// <summary>
    /// Namijenjeno za CRUD operacije na entitetu ugovor u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class izlazni_racunController : ControllerBase
    {



        private readonly izlazniracuniContext _context;
        private readonly ILogger<izlazni_racunController> _logger;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>

        public izlazni_racunController(izlazniracuniContext context, ILogger<izlazni_racunController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Dohvaća sve izlazne racune iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/izlazni_racun
        ///
        /// </remarks>
        /// <returns>izlazni racuni u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]
        public IActionResult Get()
        {

            _logger.LogInformation("Dohvaćam izlazne racune");


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var izlazni_racuni = _context.izlazni_racun
                    .Include(i => i.ugovor)
                    .ToList();



                if (izlazni_racuni == null || izlazni_racuni.Count == 0)
                {
                    return new EmptyResult();
                }

                List<izlazni_racunDTO> vrati = new();

                izlazni_racuni.ForEach(i =>
                {
                    vrati.Add(new izlazni_racunDTO()
                    {
                        ID_ugovor = i.ugovor.ID_ugovor,
                        ID_izlazni_racun=i.ID_izlazni_racun,

                        datum_usluge = (DateTime)i.datum_usluge,
                        datum_dospjeca = (DateTime)i.datum_dospjeca,
                        cijena = i.cijena,
                        broj_racuna = i.broj_racuna,
                        ugovor = i.ugovor.urudzbeni_broj

                    });
                });

                return Ok(vrati);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    ex);
            }

        }
            /// <summary>
            /// Dodaje izlaznog racuna u bazu
            /// </summary>
            /// <remarks>
            /// Primjer upita:
            ///
            ///    POST api/v1/izlazni_racun
            ///    
            ///
            /// </remarks>
            /// <returns>Kreirana izlazni racun u bazi s svim podacima</returns>
            /// <response code="200">Sve je u redu</response>
            /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
            /// <response code="503">Na azure treba dodati IP u firewall</response>

            [HttpPost]
            public IActionResult Post(izlazni_racunDTO dto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (dto.ID_ugovor <= 0)
                {
                    return BadRequest();
                }
                try
                {
                    var Ugovor = _context.ugovor.Find(dto.ID_ugovor);
                    if (Ugovor == null)
                    {
                        return BadRequest("{\"poruka\":\"Nema ugovora s tim id.\"}");
                    }
                    izlazni_racun i = new()
                    {
                        ID_izlazni_racun=dto.ID_izlazni_racun,
                        datum_usluge = (DateTime)dto.datum_usluge,
                        datum_dospjeca = (DateTime)dto.datum_dospjeca,
                        cijena=dto.cijena,
                        broj_racuna = dto.broj_racuna,
                        ugovor = Ugovor
                    };
                    _context.izlazni_racun.Add(i);
                    _context.SaveChanges();

                    dto.ugovor = Ugovor.urudzbeni_broj;

                    
                    return Ok(dto);
                }
                catch (Exception ex)
                {

                    return StatusCode(
                       StatusCodes.Status503ServiceUnavailable,
                       ex);
                }
            }
        /// <summary>
        /// Dodaje izlazni racun u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/izlazni_racun
        ///    
        ///
        /// </remarks>
        /// <returns>Kreirana TodoLista u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPut]
        [Route("ID_izlazni_racun:int")]

        public IActionResult Put(int ID_izlazni_racun, izlazni_racunDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (ID_izlazni_racun <= 0 || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var Ugovor = _context.ugovor.Find(dto.ID_ugovor);
                if (Ugovor == null)
                {
                    return BadRequest();
                }
                var vrati = _context.izlazni_racun.Find(ID_izlazni_racun);
                if (vrati == null)
                {
                    return BadRequest();
                }
                vrati.broj_racuna = dto.broj_racuna;
                vrati.ugovor = Ugovor;

                _context.izlazni_racun.Update(vrati);
                _context.SaveChanges();

                dto.ID_izlazni_racun = ID_izlazni_racun;
                dto.ugovor = Ugovor.urudzbeni_broj;

                return Ok(dto);

            }
            catch (Exception ex)
            {

                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Briše izlaznog racuna iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/izlazni_racun/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra TodoListe koja se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi TodoListe kojeu želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpDelete]
        [Route("{ID_izlazni_racun:int}")]


        [Produces("application/json")]

        public IActionResult Delete(int ID_izlazni_racun)
        {
            if (ID_izlazni_racun <= 0)
            {
                return BadRequest();
            }
            var izlazni_racun = _context.izlazni_racun.Find(ID_izlazni_racun);
            if (izlazni_racun == null)
            {
                return BadRequest();
            }
            try
            {
                _context.izlazni_racun.Remove(izlazni_racun);
                _context.SaveChanges();
                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");
            }
        }



    }




}
