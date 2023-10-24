using izlazniracuni.Data;
using izlazniracuni.Models;
using izlazniracuni.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Controllers;


namespace izlazniracuni.Controllers


{
    
    /// <summary>
 /// Namijenjeno za CRUD operacije na entitetu Todo Lista u bazi
 /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ugovorController : ControllerBase
    {
        private readonly izlazniracuniContext _context;
        private readonly ILogger<ugovorController> _logger;

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



            [HttpPost]

            public IActionResult Post(ugovor ugovor)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ugovorDTO.ID_kupac2 <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ugovori = _context.ugovor.Find(ugovorDTO.ID_kupac2);
                if (kupac2 == null)
                {
                    return BadRequest(ModelState);
                }
                ugovor u = new()
                {
                    urudzbeni_broj = ugovorDTO.urudzbeni_broj,
                    kupac2 = kupac2
                };
                _context.Todo_Lista.Add(t);
                _context.SaveChanges();

                toDoDTO.Sifra = t.Sifra;
                toDoDTO.korisnik = korisnik.Korisnicko_ime;
                return Ok(toDoDTO);
            }
            catch (Exception ex)
            {

                return StatusCode(
                   StatusCodes.Status503ServiceUnavailable,
                   ex);
            }
        }

            [HttpPut]

            [Route("{ID_ugovor:int}")]

            public IActionResult Put(int ID_ugovor, ugovor ugovor)
            {
                return StatusCode(StatusCodes.Status200OK, ugovor);
            }

            [HttpDelete]

            [Route("{ID_ugovor:int}")]
            [Produces("application/json")]

            public IActionResult Delete(int ID_ugovor)
            {
                return StatusCode(StatusCodes.Status200OK, "{\"obrisano\":true}");
            }
        }
    } 