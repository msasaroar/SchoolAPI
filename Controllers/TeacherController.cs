using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("TeacherController is working!");
        }
    }
}


/*
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
    } 
}
*/
