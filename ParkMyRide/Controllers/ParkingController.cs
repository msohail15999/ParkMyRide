using Microsoft.AspNetCore.Mvc;
using ParkMyRide.Interfaces;
using ParkMyRide.Models;
using System;

namespace ParkMyRide.Controllers
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

        [HttpPost("/parking/allocate")]
        public ActionResult<ParkingSlot> AllocateParkingSlot([FromBody] string vehicleType)
        {
            var assignedSlot = _parkingService.AllocateParkingSlot(vehicleType);
            if (assignedSlot == null)
            {
                return NotFound("No available parking slot for the given vehicle type.");
            }
            return Ok(assignedSlot.Value.slot);
        }

        [HttpPost("/parking/deallocate")]
        public ActionResult<bool> DeallocateParkingSlot([FromBody] int slotNumber)
        {
            var deallocated = _parkingService.DeallocateParkingSlot(slotNumber);
            if (!deallocated)
            {
                return NotFound("Parking slot is already vacant or not found.");
            }
            return Ok(slotNumber);
        }
    }
}
