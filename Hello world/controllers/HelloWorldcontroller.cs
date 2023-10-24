using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HelloWorld.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public string Hello()
        {
            return "Hello world";
        }

        [HttpGet]
        [Route("pozdrav")]
        public string DrugaMetoda()
        {
            return "Pozdrav svijetu";
        }

        [HttpGet]
        [Route("pozdravParametar")]
        public string DrugaMetoda(string s)
        {
            return "Hello " + s;
        }

        [HttpGet]
        [Route("pozdravViseParametara")]
        public string DrugaMetoda(string s = "", int i = 0)
        {
            return "Hello " + s + " " + i;
        }







        [HttpGet]
        [Route("{sifra:int}")]
        public string PozdravRuta(int sifra)
        {
            return "Hello " + sifra;
        }




        [HttpGet]
        [Route("{sifra:int}/{kategorija}")]
        public string PozdravRuta2(int sifra, string kategorija)
        {
            return "Hello " + sifra+ " "+ kategorija;
        }



        [HttpPost]
        public string DodavanjeNovog(string ime)
        {
            return "Dodao" + ime;

        }


        [HttpPut]
        public string Promjena(int sifra, string naziv)
        {
            return "Na šifri" + sifra + "postavljam" + naziv;
        }


        [HttpDelete]

        public bool Obrisao(int sifra)
        {
            return true;
        }



       
        
    }

}

