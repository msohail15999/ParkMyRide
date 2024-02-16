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
            _parkingSlots = InitializeParkingSlots();

        }

        private List<ParkingSlot> InitializeParkingSlots()
        {
            var parkingSlots = new List<ParkingSlot>();

            // Initialize small parking slots
            for (int i = 1; i <= 50; i++)
            {
                parkingSlots.Add(new ParkingSlot { Number = i, Type = "Small", IsOccupied = false });
            }

            // Initialize medium parking slots
            for (int i = 51; i <= 80; i++)
            {
                parkingSlots.Add(new ParkingSlot { Number = i, Type = "Medium", IsOccupied = false });
            }

            // Initialize large parking slots
            for (int i = 81; i <= 100; i++)
            {
                parkingSlots.Add(new ParkingSlot { Number = i, Type = "Large", IsOccupied = false });
            }

            return parkingSlots;
        }

        public List<ParkingSlot> GetAllParkingSlots()
        {
            return _parkingSlots;
        }

        public ParkingSlot AssignParkingSlot(string vehicleType)
        {
            // Get available slots based on vehicle type
            var availableSlots = _parkingSlots.Where(slot => !slot.IsOccupied && IsSlotCompatible(slot.Type, vehicleType)).ToList();

            if (availableSlots.Count == 0)
            {
                return null; // No available parking slot
            }

            // Assign the first available slot
            var assignedSlot = availableSlots.First();
            assignedSlot.IsOccupied = true;

            return assignedSlot;
        }


        private bool IsSlotCompatible(string slotType, string vehicleType)
        {
            if (vehicleType == "Hatchback")
            {
                return true; // All slots are compatible with hatchback
            }
            else if (vehicleType == "Sedan/Compact SUV")
            {
                return slotType != "Small"; // Sedan/Compact SUV cannot park in Small slots
            }
            else if (vehicleType == "SUV or Large")
            {
                return slotType == "Large"; // Only Large slots are compatible with SUV or Large vehicles
            }
            return false;
        }
    }
}
