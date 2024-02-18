using Xunit;
using ParkMyRide.Interfaces;
using ParkMyRide.Services;
using ParkMyRide.Models;

namespace ParkMyRideTests
{
    public class ParkingServiceTests
    {
        private IParkingService _parkingService;

        public ParkingServiceTests()
        {
            // Initialize the parking service for each test method
            _parkingService = new ParkingService();
        }

        [Fact]
        public void GetAllParkingSlots_ReturnsCorrectNumberOfSlots()
        {
            // Arrange

            // Act
            var parkingSlots = _parkingService.GetAllParkingSlots();

            // Assert
            Assert.Equal(100, parkingSlots.Count);
        }

        [Fact]
        public void AllocateParkingSlot_ReturnsSlotForHatchback()
        {
            // Arrange
            const string vehicleType = "Hatchback";

            // Act
            var allocatedSlot = _parkingService.AllocateParkingSlot(vehicleType);

            // Assert
            Assert.NotNull(allocatedSlot);
            Assert.True(_parkingService.IsSlotCompatible(allocatedSlot.Value.slot.Type, vehicleType)); // Check if the allocated slot type is compatible with the vehicle type
            Assert.True(allocatedSlot.Value.slot.IsOccupied); // Check if the allocated slot is marked as occupied
        }

        [Fact]
        public void DeallocateParkingSlot_ReturnsTrueForValidSlot()
        {
            // Arrange
            const int slotNumber = 1; // Assuming slot number 1 is already occupied
            _parkingService.AllocateParkingSlot("Hatchback");

            // Act
            var deallocated = _parkingService.DeallocateParkingSlot(slotNumber);

            // Assert
            Assert.True(deallocated); // Check if the slot was successfully deallocated
        }
    }
}
