using izlazniracuni.Data;
using izlazniracuni.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace izlazniracuni.Controllers
{

    /// <summary>
    /// Namijenjeno za CRUD operacije na entitetu kupac2 u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class kupac2Controller : ControllerBase
    {
        private readonly izlazniracuniContext _context;
        public kupac2Controller(izlazniracuniContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve kupac2 iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/kupac2
        ///
        /// </remarks>
        /// <returns>kupac2 u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpGet]
        public IActionResult Get()
        {
            // kontrola ukoliko upit nije dobar
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var kupac2 = _context.kupac2.ToList();
                if (kupac2 == null || kupac2.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.kupac2.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                    ex.Message);
            }


        }

        /// <summary>
        /// Dodaje kupac2 u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/kupac2
        ///  
        ///
        /// </remarks>
        /// <returns>Kreirani kupac2 u bazi s svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpPost]

        public IActionResult Post(kupac2 kupac2)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.kupac2.Add(kupac2);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, kupac2);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                   ex.Message);
            }

        }

        /// <summary>
        /// Mijenja podatke postojećeg kupca2 u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/kupac2/1
        ///
        /// 
        ///
        /// </remarks>
        /// <param name="id">Šifra smjera koji se mijenja</param>  
        /// <returns>Svi poslani podaci od smjera</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi smjera kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]

        [Route("{ID_kupac2:int}")]

        public IActionResult Put(int ID_kupac2, kupac2 kupac2)
        {

            if (ID_kupac2 <= 0 || kupac2 == null)
            {
                return BadRequest();
            }

            try
            {
                var kupac2Baza = _context.kupac2.Find(ID_kupac2);
                if (kupac2Baza == null)
                {
                    return BadRequest();
                }
                // inače se rade Mapper-i
                // mi ćemo za sada ručno
                kupac2Baza.ime = kupac2.ime;
                kupac2Baza.iban = kupac2.iban;
                kupac2Baza.adresa = kupac2.adresa;


                _context.kupac2.Update(kupac2Baza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, kupac2Baza);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                                  ex); // kada se vrati cijela instanca ex tada na klijentu imamo više podataka o grešci
                // nije dobro vraćati cijeli ex ali za dev je OK
            }
        }


        /// <summary>
        /// Briše kupca2 iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/kupca2/1
        ///    
        /// </remarks>
        /// <param name="id">Šifra smjera koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi smjera kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 

        [HttpDelete]

        [Route("{ID_kupac2:int}")]
        [Produces("application/json")]

        public IActionResult Delete(int ID_kupac2)
        {
            if (ID_kupac2 <= 0)
            {
                return BadRequest();
            }
            var kupac2Baza = _context.kupac2.Find(ID_kupac2);
            if (kupac2Baza == null)
            {
                return BadRequest();
            }

            try
            {
               

                _context.kupac2.Remove(kupac2Baza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");

            }
            catch (Exception ex)
            {

                return new JsonResult("{\"poruka\":\"Ne može se obrisati\"}");
            }
        }
    }
}
