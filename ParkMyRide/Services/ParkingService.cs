using ParkMyRide.Interfaces;
using ParkMyRide.Models;

namespace ParkMyRide.Services
{
    public class ParkingService : IParkingService
    {
        private List<ParkingSlot> _parkingSlots;

        public ParkingService()
        {
            // Initialize parking slots
            _parkingSlots = Enumerable.Range(1, 100).Select(n => new ParkingSlot
            {
                Number = n,
                Type = GetSlotType(n),
                IsOccupied = false
            }).ToList();
        }

        public List<ParkingSlot> GetAllParkingSlots()
        {
            return _parkingSlots;
        }

        public ParkingSlot AssignParkingSlot(string vehicleType)
        {
            // Logic to assign parking slot based on vehicle type
            throw new NotImplementedException();
        }

        private string GetSlotType(int number)
        {
            // Logic to determine slot type based on slot number
            throw new NotImplementedException();
        }
    }
}
