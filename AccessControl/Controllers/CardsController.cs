using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Controllers
{
    [ApiController]
    [Route("api/doors")]
    public class CardsController : ControllerBase
    {
        private AccessControlService _cardsService;

        public CardsController(AccessControlService cardsService)
        {
            _cardsService = cardsService;
        }

        [HttpPost]
        [Route("create123")]
        public string Create([FromQuery] int cardNumber, [FromQuery] string firstName, [FromQuery] string lastName)
        {
            return _cardsService.AddCard(cardNumber, firstName, lastName);
        }

        [HttpPost]
        public string GrantAccess([FromQuery] int cardNumber , [FromQuery] int doorNumber)
        {
            return _cardsService.GrantAccess(cardNumber, doorNumber);
        }

        [HttpPost]
        public string CancelPermission([FromQuery] string permissionId)
        {
            throw new NotImplementedException();
        }
    }
}
