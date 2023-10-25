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
    public class ugovorController : ControllerBase
    {
        private readonly izlazniracuniContext _context;
        private readonly ILogger<ugovorController> _logger;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context"></param>

        public ugovorController(izlazniracuniContext context, ILogger<ugovorController> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Dohvaća sve ugovore iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/ugovor
        ///
        /// </remarks>
        /// <returns>ugovor u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]
        public IActionResult Get()
        {

            _logger.LogInformation("Dohvaćam ugovore");


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ugovori = _context.ugovor
                    .Include(u => u.kupac2)
                    .ToList();

                if (ugovori == null || ugovori.Count == 0)
                {
                    return new EmptyResult();
                }

                List<ugovorDTO> vrati = new();

                ugovori.ForEach(u =>
                {
                    vrati.Add(new ugovorDTO()
                    {
                        ID_ugovor = u.ID_ugovor,
                        ID_kupac2 = u.kupac2.ID_kupac2,
                        datum_pocetka = (DateTime)u.datum_pocetka,
                        datum_zavrsetka = (DateTime)u.datum_zavrsetka,
                        urudzbeni_broj = u.urudzbeni_broj,
                        kupac2 = u.kupac2.ime

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

        [HttpGet]
        [Route("{ID_ugovor:int}")]
        public IActionResult GetByID(int ID_ugovor)
        {
            if (ID_ugovor <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ugovori = _context.ugovor.Find(ID_ugovor);
                if (ugovori == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, ugovori);
                }
                return new JsonResult(ugovori);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }




        /// <summary>
        /// Dodaje ugovor u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/ugovor
        ///    
        ///
        /// </remarks>
        /// <returns>Kreirana TodoLista u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]

        public IActionResult Post(ugovorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (dto.ID_kupac2 <= 0)
            {
                return BadRequest();
            }
            try
            {
                var Kupac2 = _context.kupac2.Find(dto.ID_kupac2);
                if (Kupac2 == null)
                {
                    return BadRequest("{\"poruka\":\"Nema kupca s tim id.\"}");
                }
                ugovor u = new()
                {
                    ID_ugovor = dto.ID_ugovor,
                    datum_zavrsetka=(DateTime)dto.datum_zavrsetka,
                    datum_pocetka = (DateTime)dto.datum_pocetka,
                    urudzbeni_broj = dto.urudzbeni_broj,
                    kupac2 = Kupac2
                };
                _context.ugovor.Add(u);
                _context.SaveChanges();

                dto.ID_ugovor = u.ID_ugovor;
                dto.kupac2 = Kupac2.ime;
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
        /// Dodaje ugovor u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/ugovor
        ///    {naziv:"",Korisnik:""}
        ///
        /// </remarks>
        /// <returns>Kreirana TodoLista u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]

        [Route("{ID_ugovor:int}")]

        public IActionResult Put(int ID_ugovor, ugovorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (ID_ugovor <= 0 || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var kupac2 = _context.kupac2.Find(dto.ID_kupac2);
                if (kupac2 == null)
                {
                    return BadRequest();
                }
                var vrati = _context.ugovor.Find(ID_ugovor);
                if (vrati == null)
                {
                    return BadRequest();
                }
                vrati.urudzbeni_broj = dto.urudzbeni_broj;
                vrati.kupac2 = kupac2;

                _context.ugovor.Update(vrati);
                _context.SaveChanges();

                dto.ID_ugovor = ID_ugovor;
                dto.kupac2 = kupac2.ime;

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
        /// Briše TodoListu iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/TodoLista/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra TodoListe koja se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi TodoListe kojeu želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]
        [Route("{ID_ugovor:int}")]


        [Produces("application/json")]

        public IActionResult Delete(int ID_ugovor)
        {
            if (ID_ugovor <= 0)
            {
                return BadRequest();
            }
            var ugovor = _context.ugovor.Find(ID_ugovor);
            if (ugovor == null)
            {
                return BadRequest();
            }
            try
            {
                _context.ugovor.Remove(ugovor);
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