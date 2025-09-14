namespace WebAPI.Domain.Enums;

[Flags]
public enum RoomAmenity
{
    None = 0,
    Wifi = 1,
    TV = 2,
    AirConditioning = 4,
    MiniBar = 8,
    Safe = 16,
    Balcony = 32
}
