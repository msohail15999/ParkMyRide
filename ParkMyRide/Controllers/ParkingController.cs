using Microsoft.AspNetCore.Mvc;
using ParkMyRide.Models;

namespace ParkMyRide.Interfaces
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        public ActionResult<List<ParkingSlot>> GetAllParkingSlots()
        {
            var parkingSlots = _parkingService.GetAllParkingSlots();
            return Ok(parkingSlots);
        }

        [HttpPost]
        public ActionResult<ParkingSlot> AssignParkingSlot([FromBody] string vehicleType)
        {
            var assignedSlot = _parkingService.AssignParkingSlot(vehicleType);
            if (assignedSlot == null)
            {
                return NotFound("No available parking slot for the given vehicle type.");
            }
            return Ok(assignedSlot);
        }
    }
}
