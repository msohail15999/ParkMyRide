using Microsoft.AspNetCore.Mvc;
using ParkMyRide.Interfaces;
using ParkMyRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkMyRide.Services
{
    public class ParkingService : IParkingService
    {
        private readonly object _lock = new object();
        private readonly List<ParkingSlot> _parkingSlots;

        public ParkingService()
        {
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

        public (ParkingSlot slot, int slotNumber)? AllocateParkingSlot(string vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleType))
            {
                throw new ArgumentException("Vehicle type is required.", nameof(vehicleType));
            }

            lock (_lock)
            {
                var availableSlot = _parkingSlots.FirstOrDefault(slot => !slot.IsOccupied && IsSlotCompatible(slot.Type, vehicleType));

                if (availableSlot != null)
                {
                    availableSlot.IsOccupied = true;
                    return (availableSlot, availableSlot.Number);
                }

                return null;
            }
        }

        public bool DeallocateParkingSlot(int slotNumber)
        {
            lock (_lock)
            {
                var slot = _parkingSlots.FirstOrDefault(s => s.Number == slotNumber);
                if (slot != null && slot.IsOccupied)
                {
                    slot.IsOccupied = false;
                    return true; // Deallocation successful
                }

                return false; // Parking slot was already vacant or not found
            }
        }

        public bool IsSlotCompatible(string slotType, string vehicleType)
        {
            switch (vehicleType)
            {
                case "Hatchback":
                    return true;
                case "Sedan":
                case "CompactSUV":
                    return slotType != "Small";
                case "SUV":
                case "Large":
                    return slotType == "Large";
                default:
                    return false;
            }
        }
    }
}
