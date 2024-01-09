using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomethingController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexión a base de datos establecida");

            Thread.Sleep(1000);
            Console.WriteLine("Envio de email terminado");

            Console.WriteLine("Todos los procesos han terminado");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }
    }
}
