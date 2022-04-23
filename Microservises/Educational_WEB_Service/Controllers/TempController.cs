using Lesson1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson1.Controllers
{
    [Route("api/temperature")]
    [ApiController]
    public class TempController : ControllerBase
    {
        private readonly TempRepo _tempRepo;

        public TempController(TempRepo tempRepo)
        {
            _tempRepo = tempRepo;
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Read()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

    }
}
