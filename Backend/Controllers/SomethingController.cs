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

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexión a base de datos establecida");
                return 7;
           });

            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Correo 2 enviado");
                return 2;
            });

            task1.Start();
            task2.Start();

            Console.WriteLine("Hago otra cosa");

            var result1 = await task1;
            var result2 = await task2;


            Console.WriteLine("Todo ha terminado");

            stopwatch.Stop();
            return Ok(result1 + " " + result2 + " " + stopwatch.Elapsed);
        }
    }
}
