using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        //La interfaz es la que se encarga de la inyeccion de dependencias
        private IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        //Usamos Generic (ActionResult) si no existe el recurso regresara un null 
        public ActionResult<People> Get(int id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == id);
            
            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        //Buscador
        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            //Where es una funcion de orden superior, es decir, una funcion que recibe otra funcion como parametro
            Repository.People.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if (!_peopleService.Validate(people))
            {
                return BadRequest("Name is required");
            }

            Repository.People.Add(people);

            return NoContent();
        }
    }    

    //simular una base de datos
    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1, Name = "Evelyn", Birthdate = new DateTime(2013, 12, 7)
            },
            new People()
            {
                Id = 2, Name = "Omar", Birthdate = new DateTime(2023, 2, 7)
            },
            new People()
            {
                Id = 3, Name = "Alexander", Birthdate = new DateTime(2012, 3, 19)
            },
            new People()
            {
                Id = 4, Name = "Ender", Birthdate = new DateTime(2019, 2, 17)
            }
        };
    }

    //Solo para probar el funcionamiento de la API, las clases no deben estar en el mismo archivo
    public class People
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
