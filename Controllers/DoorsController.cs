using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Controllers
{
    [ApiController]
    [Route("api/doors")]
    public class DoorsController : ControllerBase
    {
        private AccessControlService _doorsService;

        public DoorsController(AccessControlService doorsService)
        {
            _doorsService = doorsService;
        }

        [HttpPost]
        public Door Create([FromQuery] int doorNumber, [FromQuery] int doorType, [FromQuery] string doorName)
        {
            return _doorsService.AddDoor(doorNumber, doorType, doorName);
        }

        [HttpDelete]
        public string Remove([FromQuery] int doorNumber)
        {
            throw new NotImplementedException();
        }
    }
}
