using WebAPI.Domain.Enums;

namespace WebAPI.Dto.RoomTypes;

public class UpdateRoomTypeDto
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public RoomService Services { get; set; }
    public RoomAmenity Amenities { get; set; }
}
