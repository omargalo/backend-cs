using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;
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
