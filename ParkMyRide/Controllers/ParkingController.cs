using Microsoft.AspNetCore.Mvc;

namespace ParkMyRide.Interfaces
{
     [ApiController]
    [Route("api/[controller]")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        public IActionResult GetAllParkingSlots()
        {
            var parkingSlots = _parkingService.GetAllParkingSlots();
            return Ok(parkingSlots);
        }

        [HttpPost]
        public IActionResult AssignParkingSlot([FromBody] string vehicleType)
        {
            var assignedSlot = _parkingService.AssignParkingSlot(vehicleType);
            return Ok(assignedSlot);
        }
    }
}
