using DoorManipulation;
using Grpc.Net.Client;
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

        [HttpGet]
        public bool Open([FromQuery] int doorNumber, [FromQuery] string cardId)
        {
	        using (var db = new DBContext())
	        {
		        if (!db.Doors.Any(d => d.Number == doorNumber)) return false;

				var channel = GrpcChannel.ForAddress("https://localhost:7135");
				var client = new DoorControl.DoorControlClient(channel);

				var response = client.OpenDoor(new DoorRequest()
				{
					Number = doorNumber.ToString(),
					Card = cardId
				});

				if (!response.Success)
					return false;
	        }

	        return true;
        }

        [HttpGet()]
        [Route("openAll")]
        public async Task<bool> OpenAll([FromQuery] string cardId)
        {
	        var result = true;
	        using var db = new DBContext();

	        var cardProfile = db.Cards.First(c => c.Id == cardId);

	        await Parallel.ForEachAsync(cardProfile.DoorsNumbersWithAccess, (doorNumber, token) =>
	        {
		        if (!Open(doorNumber, cardId) && result) result = false;

		        return ValueTask.CompletedTask;
	        });

	        return result;
		}
	}
}
