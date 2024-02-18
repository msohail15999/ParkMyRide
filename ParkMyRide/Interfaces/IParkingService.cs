using ParkMyRide.Models;

namespace ParkMyRide.Interfaces
{
 public interface IParkingService
    {
        List<ParkingSlot> GetAllParkingSlots();
        (ParkingSlot slot, int slotNumber)? AllocateParkingSlot(string vehicleType);
        bool DeallocateParkingSlot(int slotNumber);
        bool IsSlotCompatible(string slotType, string vehicleType); // New method
    }
}
