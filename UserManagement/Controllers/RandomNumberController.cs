using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomNumberController : ControllerBase
    {
        private readonly Random random = new Random();
        [HttpGet("RandomNumber")]
        public ActionResult<int> GetRandomNumber()
        {
            int randomNumber = random.Next(1, 101);
            return Ok(randomNumber);
        }
    }
}
