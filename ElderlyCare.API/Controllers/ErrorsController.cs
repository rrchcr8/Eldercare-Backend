using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElderlyCare.API.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }

    }
}
