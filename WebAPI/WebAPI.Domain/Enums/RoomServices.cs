namespace WebAPI.Domain.Enums;

[Flags]
public enum RoomService
{
    None = 0,
    Breakfast = 1,
    Lunch = 2,
    Dinner = 4,
    Cleaning = 8,
    Laundry = 16,
    AirportPickup = 32,
    SpaAccess = 64,
    GymAccess = 128
}