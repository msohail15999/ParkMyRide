using ParkMyRide.Models;

namespace ParkMyRide.Interfaces
{
 public interface IParkingService
    {
        List<ParkingSlot> GetAllParkingSlots();
        ParkingSlot AssignParkingSlot(string vehicleType);
    }
}
